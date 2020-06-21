using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorLevelButton : MonoBehaviour
{
    private string levelName;
    private LevelEditorMenu menu;
    private bool deleteMode = false;
    public void SetLevelEditorMenu(LevelEditorMenu menu)
    {
        this.menu = menu;
    }

    public string GetName()
    {
        return levelName;
    }

    public void SetLevelName(string name)
    {
        this.levelName = name;
        this.GetComponentInChildren<Text>().text = name;
    }

    public void SwitchDeleteMode()
    {
        deleteMode = !deleteMode;
        this.GetComponent<Image>().color = deleteMode ? Color.red : Color.white;
    }

    public void ClickOnLevelButton()
    {
        if (deleteMode)
            menu.DeleteLevel(this);
        else
            menu.ChooseLevel(this);
    }
}
