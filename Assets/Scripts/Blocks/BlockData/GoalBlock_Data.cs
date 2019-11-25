﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GoalBlock_Data : Block_Data
{
    public override BlockType BlockType => BlockType.GoalBlock;
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