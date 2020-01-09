using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.ParticleSystem;

public class UpdraftBlock : Block
{
    private Block_Data updraftBlockData;
    private Collider blockCollider;
    private VelocityOverLifetimeModule velModule;
    private SizeOverLifetimeModule sizeModule;

    public override Block_Data BlockData { get => updraftBlockData; set => updraftBlockData = value; }
    [SerializeField] private float updraftVelocity = 10f;
    [SerializeField] private float particleVelocityMultiplier = 2f;
    [SerializeField] private float particleSizeMultiplier = 1.5f;
    protected override void Start()
    {
        base.Start();
        blockCollider = this.GetComponent<Collider>();
        ParticleSystem ps = this.GetComponentInChildren<ParticleSystem>();
        if(SceneManager.GetActiveScene().name == SceneDictionary.LevelEditor)
        {
            ps.gameObject.SetActive(false);
            this.GetComponent<Renderer>().enabled = true;
        }
        velModule = ps.velocityOverLifetime;
        sizeModule = ps.sizeOverLifetime;
    }

    #region Initialization / Destruction
    public override void InitializeBlock(Block_Data data)
    {
        base.InitializeBlock(data);
    }

    protected override void DestroyBlock()
    {
        base.DestroyBlock();
    }

    public override void ResetBlock()
    {
        base.ResetBlock();
        velModule.speedModifier = 1;
        sizeModule.sizeMultiplier = 1;
        blockCollider.enabled = true;
    }

    #endregion

    #region PlayerInteraction

    protected override void OnTouch(GameObject player)
    {
        if (TouchedOnGoal())
            return;
        base.OnTouch(player);
        
        Player playerScript = player.GetComponent<Player>();
        playerScript.Updraft(updraftVelocity);

        blockCollider.enabled = false;
        velModule.speedModifier = particleVelocityMultiplier;
        sizeModule.sizeMultiplier = particleSizeMultiplier;


    }

    protected override void OnTouchEnd(GameObject player)
    {
        base.OnTouchEnd(player);
    }

    #endregion

    #region Helper

    protected override void SpawnDestructionEffect()
    {
    }

    #endregion


}
