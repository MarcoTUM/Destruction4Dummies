using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RestoreableBlock_Data : Block_Data
{
    public static Color[] RestoreableBlockColors = new Color[5] { Color.white, Color.red, Color.green, Color.magenta, Color.blue };
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
        return base.Equals(other) && this.blockID == ((KeyBlock_Data)other).GetID();
    }
}