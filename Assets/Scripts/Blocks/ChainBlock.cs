using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBlock : Block
{
    private Block_Data chainBlockData = new ChainBlock_Data();
    public override Block_Data BlockData { get => chainBlockData; set => chainBlockData = value; }

    [SerializeField]
    private uint blockID = 0;

    #region Initialization / Destruction
    public override void InitializeBlock(Block_Data data)
    {
        base.InitializeBlock(data);
        this.blockID = ((ChainBlock_Data)data).GetChainID();
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
        //base.OnTouch(player);

        GameObject[] chainBlocks = GameObject.FindGameObjectsWithTag("ChainBlock");
        foreach (GameObject chainBlock in chainBlocks)
        {
            ChainBlock chainBlockScript = chainBlock.GetComponent<ChainBlock>();

            if (chainBlockScript.blockID == ((ChainBlock_Data)BlockData).GetChainID())
            {
                chainBlockScript.StartCoroutine("StartBlockDestruction");
            }
        }
        
    }

    protected override void OnTouchEnd(GameObject player)
    {
        base.OnTouchEnd(player);
    }

    #endregion


}
