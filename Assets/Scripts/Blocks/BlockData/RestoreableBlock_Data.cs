using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RestoreableBlock_Data : Colorable_Block_Data
{
    public override BlockType BlockType => BlockType.Restoreable;
    private uint blockID = 0;
    public RestoreableBlock_Data()
    {

    }

    public RestoreableBlock_Data(uint blockID)
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
        return base.Equals(other) && this.blockID == ((RestoreableBlock_Data)other).GetID();
    }
}