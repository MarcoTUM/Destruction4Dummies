using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EmptyBlock_Data : Block_Data
{
    public override BlockType BlockType => BlockType.EmptyBlock;
}
