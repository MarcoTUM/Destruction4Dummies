using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomLevelButton : MonoBehaviour
{
    private string levelName;

    public void SetLevelName(string name)
    {
        this.levelName = name;
        this.GetComponentInChildren<Text>().text = name;
    }

    public void ClickOnCustomLevelButton()
    {
        Gamemaster.Instance.SetNextCustomLevelToLoad(levelName);
        SceneManager.LoadScene(SceneDictionary.Play);
    }
}
