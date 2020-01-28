using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeRotationLock : MonoBehaviour
{
    public Quaternion rotation;
    void Update()
    {
        transform.rotation = rotation;
    }
}
