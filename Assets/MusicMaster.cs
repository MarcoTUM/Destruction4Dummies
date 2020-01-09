using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicMaster : Singleton<MusicMaster>
{

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name != "PlayScene")
        {
            //stop music
            transform.GetComponent<AudioSource>().Stop();
            return;
        }
        transform.GetComponent<AudioSource>().Play();
    }
}
