using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBlock : Block
{
    private Block_Data stoneBlockData = new StoneBlock_Data();
    public override Block_Data BlockData { get => stoneBlockData; set => stoneBlockData = value; }


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
        base.OnTouch(player);
    }

    protected override void OnTouchEnd(GameObject player)
    {
        base.OnTouchEnd(player);
        //Instantiate(EffectManager.Instance.GetEffect(4), transform.position, Quaternion.identity);
    }

    #endregion

    #region Helper

    protected override void SpawnDestructionEffect()
    {
        Instantiate(EffectManager.Instance.GetEffect(5), transform.position, Quaternion.identity);
    }

    #endregion
}
