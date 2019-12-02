using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour
{
    [SerializeField] private float gameOverDuration = 1f;
    [SerializeField] private float respawnDuration = 1f;
    [SerializeField] private float gameOverDistance = 5;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private Transform player; //change to Gamemaster.Instance
    [SerializeField] private GameObject deathAnim;
    private Level level;
    private Vector3 spawnPosition;
    private bool running = true;
    private void Start()
    {
        level = Gamemaster.Instance.GetLevel();
        Gamemaster.Instance.CreatePlayLevel();
        StartCoroutine(Gamemaster.Instance.GetCameraPlayControl().PlayLevelOpening());
        Vector2Int startPlatformCoord = level.GetLevelData().StartPlatformCoordinates;
        spawnPosition = new Vector3(startPlatformCoord.x, startPlatformCoord.y + 1f, 0) * Block_Data.BlockSize;//replace 1f with player.height
    }

    private void Update()
    {
        if(running && player.position.y < level.transform.position.z - gameOverDistance)
        {
            StartCoroutine(PlayerDeath());
        }
    }

    private IEnumerator PlayerDeath()
    {
        running = false;
        player.gameObject.SetActive(false);
        GameObject deathAnimInstance = Instantiate(deathAnim);
        deathAnimInstance.transform.position = player.transform.position;
        sfxSource.clip = deathSound;
        sfxSource.Play();
        yield return new WaitForSeconds(gameOverDuration);
        level.ResetLevel();
        player.transform.position = spawnPosition;
        StartCoroutine(Gamemaster.Instance.GetCameraPlayControl().FocusCameraOnPlayer(respawnDuration));
        yield return new WaitForSeconds(respawnDuration);
        player.gameObject.SetActive(true);

        running = true;
    }

    private IEnumerator PlayerFadeIn() //test when playerModel available
    {
        float timer = 0;
        Color playerColor = Color.grey;
        while (timer < respawnDuration)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            playerColor.a = timer / respawnDuration;
            player.GetComponent<Renderer>().material.color = playerColor;
        }
        player.GetComponent<Renderer>().material.color = Color.white;
    }

}
