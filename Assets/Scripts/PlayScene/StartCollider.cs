using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCollider : MonoBehaviour
{

    public void SetPosition(Vector3 position)
    {
        this.transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagDictionary.Player)
            Gamemaster.Instance.GetCameraPlayControl().OnStartPlatform = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == TagDictionary.Player)
            Gamemaster.Instance.GetCameraPlayControl().OnStartPlatform = false;
    }

    private void OnDestroy()
    {
        if(Gamemaster.Instance != null && Gamemaster.Instance.GetCameraPlayControl() != null)
            Gamemaster.Instance.GetCameraPlayControl().OnStartPlatform = false;
    }
}
