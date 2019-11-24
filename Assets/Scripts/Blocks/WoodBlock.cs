using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBlock : Block
{
    Block_Data woodBlockData = new WoodBlock_Data();
    public override Block_Data BlockData { get => woodBlockData; set => woodBlockData = value; }

    protected override void OnTouch()
    {
        throw new System.NotImplementedException();
    }

    public override void ResetBlock()
    {
    }

    public override void InitializeBlock(Block_Data data)
    {
        base.InitializeBlock(data);
    }

}
