using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayCameraControl : MonoBehaviour
{
    [SerializeField] private float zoomOutTime = 1f;   
    [SerializeField] private float zoomInTime = 1f;   
    [SerializeField] private float smoothTime = .5f;
    [SerializeField] private float cameraDistance = 10f;
    [SerializeField] private float openingDuration = 2f;
    [SerializeField] private Vector2 openingMargin = new Vector2(5,5); //margin from level border to camera view frustum border
    private new Camera camera;
    private Player player;
    private Vector3 velocity = Vector3.zero;
    private bool cameraFollow = true;
    private float maxZoomDistance { get => (OnStartPlatform ? centerZoomDistance : fixedZoomDistance); }
    private const float fixedZoomDistance = 12;
    private float centerZoomDistance;
    private float currentZoomDistance;
    private Coroutine zoomRoutine;
    
    private bool isZoomingOut = false;
    private Vector3 levelCenter;
    public bool OnStartPlatform { get; set; }
    private void Awake()
    {
        camera = this.GetComponent<Camera>();
        Gamemaster.Instance.Register(this);
    }

    private void Start()
    {
        player = Gamemaster.Instance.GetPlayer();
        float x = Gamemaster.Instance.GetLevel().GetWorldWidth()/2f;
        float y = Gamemaster.Instance.GetLevel().GetWorldHeight() / 2f;
        float z = 0;
        levelCenter = new Vector3(x, y, z);
    }

    private void Update()
    {
        if (cameraFollow)
        {
            if(isZoomingOut && OnStartPlatform)
                SmoothDampToPosition(levelCenter - (cameraDistance + currentZoomDistance) * Vector3.forward);
            else
                SmoothDampToPosition(player.transform.position - (cameraDistance + currentZoomDistance / 2f) * Vector3.forward);
        }
    }

    /// <summary>
    /// Shows the whole level and zooms in on Player on startPlatform => activates Player
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayLevelOpening(Player player)
    {
        cameraFollow = false;
        Vector2Int dim = Gamemaster.Instance.GetLevel().GetLevelDimensions();
        float startFrustumValue = dim.y + openingMargin.y;
        if (dim.x > dim.y)
        {
            startFrustumValue = dim.x / camera.aspect + openingMargin.x;
        }

        float startDistance = startFrustumValue / 2f / Mathf.Tan(camera.fieldOfView * 0.5f * (Mathf.PI / 180f));
        centerZoomDistance = startDistance - cameraDistance;
        this.transform.position = new Vector3(dim.x / 2f * Block_Data.BlockSize, dim.y / 2f * Block_Data.BlockSize, -startDistance);
        
        float timer = 0;
        while(timer < openingDuration)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            SmoothDampToPosition(player.transform.position - cameraDistance * Vector3.forward, openingDuration - timer);
        }
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

    private void SmoothDampToPosition(Vector3 targetPosition, float smoothTime)
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void SmoothDampToPosition(Vector3 targetPosition)
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    #region Zoom
    public void StartZoomOut()
    {
        if (!cameraFollow)
            return;
        if (zoomRoutine != null)
            StopCoroutine(zoomRoutine);
        isZoomingOut = true;
        zoomRoutine = StartCoroutine(ZoomOut());
    }

    private IEnumerator ZoomOut()
    {
        float startPerc = (-cameraDistance - this.transform.position.z) / maxZoomDistance;
        float timer = startPerc * zoomOutTime;

        while(timer < zoomOutTime)
        {
            currentZoomDistance = Mathf.Lerp(0, maxZoomDistance, timer / zoomOutTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime * (OnStartPlatform ? 1 : 2);
        }
        currentZoomDistance = maxZoomDistance;
    }

    public void StartZoomIn()
    {
        if (!cameraFollow || zoomRoutine == null)
            return;

        StopCoroutine(zoomRoutine);
        isZoomingOut = false;
        zoomRoutine = StartCoroutine(ZoomIn());
    }

    private IEnumerator ZoomIn()
    {
        float startPerc = (-cameraDistance - this.transform.position.z) / maxZoomDistance;
        float timer = startPerc * zoomInTime;

        while (timer > 0)
        {
            currentZoomDistance = Mathf.Lerp(0, maxZoomDistance, timer / zoomInTime);
            yield return new WaitForEndOfFrame();
            timer -= Time.deltaTime  * (OnStartPlatform ? 1 : 2);
        }
        currentZoomDistance = 0;
    }
    #endregion
}
