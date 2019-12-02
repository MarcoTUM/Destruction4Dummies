using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayCameraControl : MonoBehaviour
{
    [SerializeField] Transform player; //Change to player registered in Gamemaster after Jonathan merge
    [SerializeField] private float smoothTime = .5f;
    [SerializeField] private float cameraDistance = 10f;
    [SerializeField] private float openingDuration = 2f;
    [SerializeField] private Vector2 openingMargin = new Vector2(5,5); //margin from level border to camera view frustum border
    private new Camera camera;
    private Vector3 velocity = Vector3.zero;
    private bool cameraFollow = true;

    private void Awake()
    {
        camera = this.GetComponent<Camera>();
        Gamemaster.Instance.Register(this);
    }

    /// <summary>
    /// Shows the whole level and zooms in on Player on startPlatform => activates Player
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayLevelOpening()
    {
        cameraFollow = false;
        Vector2Int dim = Gamemaster.Instance.GetLevel().GetLevelDimensions();
        float startFrustumValue = dim.y + openingMargin.y;
        if (dim.x > dim.y)
        {
            startFrustumValue = dim.x / camera.aspect + openingMargin.x;
        }

        float startDistance = startFrustumValue / 2f / Mathf.Tan(camera.fieldOfView * 0.5f * (Mathf.PI / 180f));
        this.transform.position = new Vector3(dim.x / 2f * Block_Data.BlockSize, dim.y / 2f * Block_Data.BlockSize, -startDistance);
        
        float timer = 0;
        player.gameObject.SetActive(false);
        while(timer < openingDuration)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            SmoothDampToPosition(player.transform.position - cameraDistance * Vector3.forward, openingDuration - timer);
        }
        player.gameObject.SetActive(true);
        cameraFollow = true;
    }

    /// <summary>
    /// Forces camera to move to playerPosition in time of duration
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public IEnumerator FocusCameraOnPlayer(float duration)
    {
        cameraFollow = false;
        float timer = 0;
        while (timer < duration)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            SmoothDampToPosition(player.transform.position - cameraDistance * Vector3.forward, duration - timer);
        }
        cameraFollow = true;
    }

    private void Update()
    {
        if (cameraFollow)
            SmoothDampToPosition(player.transform.position - cameraDistance * Vector3.forward);
    }

    private void SmoothDampToPosition(Vector3 targetPosition, float smoothTime)
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void SmoothDampToPosition(Vector3 targetPosition)
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
