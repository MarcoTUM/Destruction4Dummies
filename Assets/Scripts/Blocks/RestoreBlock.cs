using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreBlock : Block
{
    private Block_Data restoreBlockData = new RestoreBlock_Data();
    public override Block_Data BlockData { get => restoreBlockData; set => restoreBlockData = value; }

    [SerializeField]
    private uint blockID = 0;

    #region Initialization / Destruction
    public override void InitializeBlock(Block_Data data)
    {
        base.InitializeBlock(data);
        this.blockID = ((RestoreBlock_Data)data).GetID();
        this.GetComponent<Renderer>().material.color = RestoreBlock_Data.RestoreBlockColors[this.blockID - 1];
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
        GameObject[] blocks = GameObject.FindGameObjectsWithTag(TagDictionary.RestoreableBlock);
        foreach (GameObject block in blocks)
        {
            RestoreableBlock blockScript = block.GetComponent<RestoreableBlock>();
            RestoreableBlock_Data restoreableBlockData = (RestoreableBlock_Data)blockScript.BlockData;

            if (restoreableBlockData.GetID() == ((RestoreBlock_Data)BlockData).GetID())
            {
                blockScript.RestoreBlock();
            }
        }
    }

    protected override void OnTouchEnd(GameObject player)
    {
        base.OnTouchEnd(player);
    }

    #endregion
}