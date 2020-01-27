using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlock : Block
{
    private Block_Data startBlockData = new EmptyBlock_Data();
    public override Block_Data BlockData { get => startBlockData; set => startBlockData = value; }
    
    public static bool PlayerIsOnStart() { return touchedBlocks > 0; }
    private static int touchedBlocks = 0;

    #region PlayerInteraction

    protected override void OnTouch(GameObject player)
    {
        if (TouchedOnGoal())
            return;
        touchedBlocks++;
    }

    protected override void OnTouchEnd(GameObject player)
    {
        touchedBlocks--;
    }

    #endregion
}
