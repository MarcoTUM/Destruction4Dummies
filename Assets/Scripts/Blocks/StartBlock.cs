using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlock : Block
{
    private Block_Data startBlockData = new EmptyBlock_Data();
    public override Block_Data BlockData { get => startBlockData; set => startBlockData = value; }

    #region PlayerInteraction
    
    protected override void OnTouch(GameObject player)
    {
        if (TouchedOnGoal())
            return;
    }

    protected override void OnTouchEnd(GameObject player)
    {

    }

    #endregion
}
