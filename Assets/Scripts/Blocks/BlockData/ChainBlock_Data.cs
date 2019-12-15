using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChainBlock_Data : Block_Data
{
    public static Color[] ChainBlockColors = new Color[5] { Color.white, Color.red, Color.green, Color.magenta, Color.blue };
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
