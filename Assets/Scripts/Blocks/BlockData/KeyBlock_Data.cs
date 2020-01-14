using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyBlock_Data : Colorable_Block_Data
{
    public override BlockType BlockType => BlockType.Key;
    private uint blockID = 0;
    public KeyBlock_Data()
    {

    }

    public KeyBlock_Data(uint blockID)
    {
        this.blockID = blockID;
    }

    public void SetID(uint blockID)
    {
        this.blockID = blockID;
    }

    public uint GetID()
    {
        return blockID;
    }

    public override bool Equals(Block_Data other)
    {
        return base.Equals(other) && this.blockID == ((KeyBlock_Data)other).GetID();
    }
}