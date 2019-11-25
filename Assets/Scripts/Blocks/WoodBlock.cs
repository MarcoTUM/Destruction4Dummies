using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBlock : Block
{
    Block_Data woodBlockData = new WoodBlock_Data();
    public override Block_Data BlockData { get => woodBlockData; set => woodBlockData = value; }


    #region Initialization / Destruction
    public override void InitializeBlock(Block_Data data)
    {
        base.InitializeBlock(data);
    }

    protected virtual void DestroyBlock()
    {
        base.DestroyBlock();
    }

    public override void ResetBlock()
    {
        base.ResetBlock();
    }

    #endregion

    #region PlayerInteraction

    protected override void OnTouch()
    {
        base.OnTouch();
    }

    protected override void OnTouchEnd()
    {
        base.OnTouchEnd();
    }
    #endregion


}
