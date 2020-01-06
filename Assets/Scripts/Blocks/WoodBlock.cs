using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBlock : Block
{
    private Block_Data woodBlockData = new ChainBlock_Data();
    public override Block_Data BlockData { get => woodBlockData; set => woodBlockData = value; }

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
            Instantiate(EffectManager.Instance.GetEffect(6), transform.position, Quaternion.identity);
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
        Instantiate(EffectManager.Instance.GetEffect(3), transform.position, Quaternion.identity);
    }

    #endregion


}
