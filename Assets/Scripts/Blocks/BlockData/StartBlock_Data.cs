using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StartBlock_Data : Block_Data
{
    public override bool IsReplaceable { get => false; }
    public override BlockType BlockType => BlockType.Start;
}
