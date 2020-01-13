using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RestoreableBlock_Data : Block_Data
{
    public static Color[] RestoreableBlockColors = new Color[5] {
        Color.white, new Vector4(1, 0.5f, 0.5f, 1), new Vector4(0.5f, 1, 0.5f, 1), new Vector4(1, 0.5f, 1, 1), new Vector4(0.5f, 0.5f, 1, 1)
    };
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