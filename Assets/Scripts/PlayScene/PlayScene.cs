using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour
{
    [SerializeField] private float gameOverDuration = 1f;
    [SerializeField] private float respawnDuration = 1f;
    [SerializeField] private float gameOverDistance = 5;
    [SerializeField] private StartCollider startCollider;
    [HideInInspector] public Advisor advisor;
    private Player player;
    private Level level;
    private Vector3 spawnPosition;
    private bool running = true;

    private IEnumerator forceOutbreakCoroutine;

    private void Start()
    {
        player = Gamemaster.Instance.GetPlayer();
        level = Gamemaster.Instance.GetLevel();
        advisor = GameObject.FindObjectOfType<Advisor>();

        Gamemaster.Instance.CreatePlayLevel();
        advisor?.InitializePosition();
        StartCoroutine(Gamemaster.Instance.GetCameraPlayControl().PlayLevelOpening(player));
        Vector2Int startCoord = level.GetLevelData().StartPlatformCoordinates;
        player.SetStartPlatform(new Vector3(startCoord.x, startCoord.y, 0));
        startCollider.SetPosition(new Vector3(startCoord.x, startCoord.y, 0));
        player.SpawnAtSpawnPlatform();
    }

    private void Update()
    {
        if (running && player.transform.position.y < level.GetFallBoundary() - gameOverDistance)
        {
            StartCoroutine(PlayerDeath());
        }
    }

    public void KillPlayer()
    {
        if (!running || player.IsOnGoal)
            return;
        StartCoroutine(PlayerDeath());
    }

    private IEnumerator PlayerDeath()
    {
        StopForceOutbreak();
        advisor?.HandlePlayerDeath();
        running = false;
        player.gameObject.SetActive(false);
        Instantiate(EffectManager.Instance.GetEffect(2),player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(gameOverDuration);
        level.ResetLevel();

        player.SpawnAtSpawnPlatform();
        StartCoroutine(Gamemaster.Instance.GetCameraPlayControl().FocusCameraOnPlayer(respawnDuration));
        yield return new WaitForSeconds(respawnDuration);
        Instantiate(EffectManager.Instance.GetEffect(13), player.transform.position, Quaternion.identity);
        player.gameObject.SetActive(true);
        running = true;
    }

    #region BlockHelperFunctions

    public void RespawnRespawnBlocks(RespawnBlock respawnBlock, float respawnTime)
    {
        StartCoroutine(RespawnBlocks(respawnBlock, respawnTime));
    }

    private IEnumerator RespawnBlocks(RespawnBlock respawnBlock, float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);
        respawnBlock.ResetBlock();
    }

    /// <summary>
    /// Forces the player to outbreak power destroying all blocks in a certain radius.
    /// </summary>
    /// <param name="chargeTime">Chare time</param>
    /// <param name="outbreakRadius">Outbreak radius</param>
    /// <param name="forceOutbreak">Force outbreak partcile system</param>
    public void StartForceOutbreak(float chargeTime, float outbreakRadius, ParticleSystem forceOutbreak)
    {
        forceOutbreakCoroutine = ForceOutbreak(chargeTime, outbreakRadius, forceOutbreak);
        StartCoroutine(forceOutbreakCoroutine);
    }

    public void StopForceOutbreak()
    {
        try
        {
            StopCoroutine(forceOutbreakCoroutine);
        }
        catch (NullReferenceException) { }
    }

    private IEnumerator ForceOutbreak(float chargeTime, float outbreakRadius, ParticleSystem forceOutbreak)
    {
        yield return new WaitForSeconds(chargeTime);
        
        // Get the player
        Player player = Gamemaster.Instance.GetPlayer();
        if (player.IsOnGoal)
            yield break;
        // Get all colliders that overlap a sphere of radius = outbreakRadius
        Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, outbreakRadius);

        // Instantiate force outbreak particle effect
        Instantiate(forceOutbreak, player.transform.position, Quaternion.identity);

        // For each collider destroy the block
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider != null && !hitCollider.gameObject.CompareTag("Player"))
            {
                if (hitCollider.TryGetComponent<Block>(out Block block))
                {
                    block.StartInstantBlockDestruction();
                }
            }
        }
    }

    #endregion
}
