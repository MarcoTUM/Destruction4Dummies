using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GoalBlock_Data : Block_Data
{
    public override bool IsReplaceable { get => false; }
    public override BlockType BlockType => BlockType.Goal;
    public string NextLevel;

    public GoalBlock_Data()
    {
        this.NextLevel = "";
    }

    public GoalBlock_Data(string nextLevel)
    {
        this.NextLevel = nextLevel;
    }
}
