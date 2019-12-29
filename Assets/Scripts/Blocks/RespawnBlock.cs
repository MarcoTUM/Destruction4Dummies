using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBlock : Block
{
    private Block_Data respawnBlockData = new RespawnBlock_Data();
    public override Block_Data BlockData { get => respawnBlockData; set => respawnBlockData = value; }

    [SerializeField]
    private float respawnTime;

    #region Initialization / Destruction
    public override void InitializeBlock(Block_Data data)
    {
        // Negative respawnTime makes no sence
        if (respawnTime < 0)
            throw new ArgumentOutOfRangeException("respawnTime", "Negative respawnTime makes no sence.");

        ((RespawnBlock_Data)BlockData).SetRespawnTime(respawnTime);
        base.InitializeBlock(data);
    }

    protected override void DestroyBlock()
    {
        GameObject.FindObjectOfType<PlayScene>().RespawnRespawnBlocks(this, respawnTime);
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
    }

    #endregion
}
