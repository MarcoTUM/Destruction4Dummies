using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheat : MonoBehaviour
{
   
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if(Input.GetKeyDown(KeyCode.End))
            {
                Gamemaster.Instance.SetProgress(99);
                SceneManager.LoadScene(SceneDictionary.MainMenu);
            }
            else if (Input.GetKeyDown(KeyCode.Delete))
            {
                Gamemaster.Instance.SetProgress(0);
                SceneManager.LoadScene(SceneDictionary.MainMenu);
            }
        }
    }
}
