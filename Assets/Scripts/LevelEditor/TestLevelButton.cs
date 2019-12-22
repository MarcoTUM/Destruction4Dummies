using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLevelButton : MonoBehaviour
{
    public void TestLevel()
    {
        Gamemaster.Instance.GetLevelEditor().SaveLevel();
        Gamemaster.Instance.SetNextTestLevelToLoad();
        SceneManager.LoadScene(SceneDictionary.Play);
    }
}
