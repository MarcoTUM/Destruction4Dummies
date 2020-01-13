using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Colorable_Block_Data : Block_Data
{
    public static Color[] BlockColors = new Color[5] {
        Color.white, new Vector4(1, 0.5f, 0.5f, 1), new Vector4(0.5f, 1, 0.5f, 1), new Vector4(1, 0.5f, 1, 1), new Vector4(0.5f, 0.5f, 1, 1)
    };
}
