using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainLevelButton : MonoBehaviour
{
    private int levelId;

    public void SetLevelId(int id)
    {
        this.levelId = id;
        this.GetComponentInChildren<Text>().text = "Level " + id;
    }

    public void ClickOnMainLevelButton()
    {
        Gamemaster.Instance.SetNextMainLevelToLoad(levelId);
        SceneManager.LoadScene(SceneDictionary.Play);
    }
}
