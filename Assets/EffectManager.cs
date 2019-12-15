using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    public GameObject[] effects;

    public GameObject GetEffect(int index)
    {
        return effects[index];
    }
}
