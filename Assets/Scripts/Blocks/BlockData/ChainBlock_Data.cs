using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChainBlock_Data : Block_Data
{
    public static Color[] ChainBlockColors = new Color[5] {
        Color.white, new Vector4(1, 0.5f, 0.5f, 1), new Vector4(0.5f, 1, 0.5f, 1), new Vector4(1, 0.5f, 1, 1), new Vector4(0.5f, 0.5f, 1, 1)
    };
    public override BlockType BlockType => BlockType.Chain;
    private uint blockID = 0;
    public ChainBlock_Data()
    {

    }

    public ChainBlock_Data(uint blockID)
    {
        this.blockID = blockID;
    }

    public void SetChainID(uint blockID)
    {
        this.blockID = blockID;
    }

    public uint GetChainID()
    {
        return blockID;
    }

    public override bool Equals(Block_Data other)
    {
        return base.Equals(other) && this.blockID == ((ChainBlock_Data)other).GetChainID();
    }
}
