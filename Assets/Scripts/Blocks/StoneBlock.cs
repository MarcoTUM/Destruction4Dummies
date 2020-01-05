using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBlock : Block
{
    private Block_Data stoneBlockData = new StoneBlock_Data();
    public override Block_Data BlockData { get => stoneBlockData; set => stoneBlockData = value; }

    public AudioClip audioClip;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
    }

    #endregion

    #region PlayerInteraction

    protected override void OnTouch(GameObject player)
    {
        if (Gamemaster.Instance.GetPlayer().canDestroy)
        {
            audioSource.PlayOneShot(audioClip, Random.Range(0.5f, 1.5f));
        }
        base.OnTouch(player);
    }

    protected override void OnTouchEnd(GameObject player)
    {
        base.OnTouchEnd(player);
    }

    #endregion

    #region Helper

    protected override void SpawnDestructionEffect()
    {
        Instantiate(EffectManager.Instance.GetEffect(5), transform.position, Quaternion.identity);
    }

    #endregion
}
