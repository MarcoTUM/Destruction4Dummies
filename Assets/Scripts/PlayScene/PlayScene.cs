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
        StartCoroutine(ForceOutbreak(chargeTime, outbreakRadius, forceOutbreak));
    }

    private IEnumerator ForceOutbreak(float chargeTime, float outbreakRadius, ParticleSystem forceOutbreak)
    {
        yield return new WaitForSeconds(chargeTime);

        // Get the player script (assuming there is always only one player in the scene)
        Player player = GameObject.FindObjectOfType<Player>();

        // Get all colliders that overlap a sphere of radius = outbreakRadius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, outbreakRadius);

        // Instantiate force outbreak particle effect
        Instantiate(forceOutbreak, player.transform.position, Quaternion.identity);

        // For each collider destroy the block
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Block"))
            {
                hitCollider.gameObject.GetComponent<Block>().StartBlockDestructionCoroutine();
            }
        }
    }

    #endregion
}
