using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainLevelButton : MonoBehaviour, ISelectHandler
{
    private int levelId;
    private MainLevelSelection mainLevelSelection;
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

    public void SetSelection(MainLevelSelection selection)
    {
        mainLevelSelection = selection;
    }

    public void OnSelect(BaseEventData eventData)
    {
        if(Gamemaster.Instance.IsUsingXbox)
            mainLevelSelection.SelectButton(levelId);
    }

}
