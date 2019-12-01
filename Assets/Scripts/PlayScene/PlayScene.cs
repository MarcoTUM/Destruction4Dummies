using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour
{
    void Start()
    {
        Gamemaster.Instance.CreatePlayLevel();
        StartCoroutine(Gamemaster.Instance.GetCameraPlayControl().PlayLevelOpening());
        
    }
    
}
