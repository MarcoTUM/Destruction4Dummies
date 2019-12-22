using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomLevelButton : MonoBehaviour
{
    private string levelName;
    private bool deleteMode = false;
    private CustomLevelSelection selection;
    public void SetLevelName(string name)
    {
        this.levelName = name;
        this.GetComponentInChildren<Text>().text = name;
    }

    public void SetLevelSelection(CustomLevelSelection selection)
    {
        this.selection = selection;
    }

    public void SwitchDeleteMode()
    {
        deleteMode = !deleteMode;
        this.GetComponent<Image>().color = deleteMode ? Color.red : Color.white;
    }

    public void ClickOnCustomLevelButton()
    {
        if (deleteMode)
        {
            LevelSaveLoad.Delete(levelName, FilePaths.CustomPlayLevelFolder);
            selection.RemoveLevel(this);
            Destroy(this.gameObject);
        }
        else
        {
            Gamemaster.Instance.SetNextCustomLevelToLoad(levelName);
            SceneManager.LoadScene(SceneDictionary.Play);
        }
    }
}
