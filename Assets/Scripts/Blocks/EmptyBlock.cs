using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyBlock : Block
{
    Block_Data emptyBlockData = new EmptyBlock_Data();
    public override Block_Data BlockData { get => emptyBlockData; set => emptyBlockData = value; }

    public override void ResetBlock()
    {
        throw new System.NotImplementedException();
    }

    public override void InitializeBlock(Block_Data data)
    {
        base.InitializeBlock(data);
    }

    protected override void OnTouch()
    {
        
    }
    
}
