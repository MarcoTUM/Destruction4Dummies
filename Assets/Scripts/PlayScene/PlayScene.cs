using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour
{
    [SerializeField] private float gameOverDuration = 1f;
    [SerializeField] private float respawnDuration = 1f;
    [SerializeField] private float gameOverDistance = 5;
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
        Vector2Int startCoord = level.GetLevelData().StartPlatformCoordinates;
        player.SetStartPlatform(new Vector3(startCoord.x, startCoord.y, 0));
        player.SpawnAtSpawnPlatform();
    }

    private void Update()
    {
        if (running && player.transform.position.y < level.transform.position.z - gameOverDistance)
        {
            StartCoroutine(PlayerDeath());
        }
    }

    public void KillPlayer()
    {
        if (!running)
            return;
        StartCoroutine(PlayerDeath());
    }

    private IEnumerator PlayerDeath()
    {
        running = false;
        player.gameObject.SetActive(false);
        Instantiate(EffectManager.Instance.GetEffect(2),player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(gameOverDuration);
        level.ResetLevel();

        player.SpawnAtSpawnPlatform();
        StartCoroutine(Gamemaster.Instance.GetCameraPlayControl().FocusCameraOnPlayer(respawnDuration));
        yield return new WaitForSeconds(respawnDuration);
        player.gameObject.SetActive(true);
        running = true;
    }
    

  

}
