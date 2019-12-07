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
    [SerializeField] private GameObject deathAnim;

    private Player player;
    private Level level;
    private Vector3 spawnPosition;
    private bool running = true;

    private void Start()
    {
        player = Gamemaster.Instance.GetPlayer();
        level = Gamemaster.Instance.GetLevel();
        Gamemaster.Instance.CreatePlayLevel();
        StartCoroutine(Gamemaster.Instance.GetCameraPlayControl().PlayLevelOpening(player));
        Gamemaster.Instance.GetPlayer().SpawnAtStartPlatform();
    }

    private void Update()
    {
        if (running && player.transform.position.y < level.transform.position.z - gameOverDistance)
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

  

}
