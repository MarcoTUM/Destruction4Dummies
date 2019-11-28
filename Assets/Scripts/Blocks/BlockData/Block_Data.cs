using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Block_Data
{
    public const int BlockSize = 1;
    public abstract BlockType BlockType { get; }
}
