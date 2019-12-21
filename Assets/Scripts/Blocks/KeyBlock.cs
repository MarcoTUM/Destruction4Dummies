using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBlock : Block
{
    private Block_Data keyBlockData = new KeyBlock_Data();
    public override Block_Data BlockData { get => keyBlockData; set => keyBlockData = value; }

    [SerializeField]
    private uint blockID = 0;

    #region Initialization / Destruction
    public override void InitializeBlock(Block_Data data)
    {
        base.InitializeBlock(data);
        this.blockID = ((KeyBlock_Data)data).GetID();
        this.GetComponent<Renderer>().material.color = KeyBlock_Data.KeyBlockColors[this.blockID - 1];
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
        GameObject[] blocks = GameObject.FindGameObjectsWithTag(TagDictionary.LockBlock);
        foreach (GameObject block in blocks)
        {
            LockBlock blockScript = block.GetComponent<LockBlock>();
            LockBlock_Data lockBlockData = (LockBlock_Data)blockScript.BlockData;

            if (lockBlockData.GetID() == ((KeyBlock_Data)BlockData).GetID())
            {
                blockScript.UnlockBlock();
                // blockScript.StartCoroutine("StartBlockDestruction");
            }
        }
        base.OnTouch(player);
    }

    protected override void OnTouchEnd(GameObject player)
    {
        base.OnTouchEnd(player);
    }

    #endregion
}
