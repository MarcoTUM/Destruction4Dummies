using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MainMenuWindow { Main, LevelSelection };

public class MainMenuGUI : MonoBehaviour
{
    public static MainMenuWindow CurrentActiveWindow = MainMenuWindow.Main;
    [SerializeField] private GameObject mainWindow, levelSelectionWindow;

    private void Start()
    {
        if (CurrentActiveWindow == MainMenuWindow.Main)
            SwitchToMain();
        else if (CurrentActiveWindow == MainMenuWindow.LevelSelection)
            SwitchToLevelSelection();
    }

    public void SwitchToLevelSelection()
    {
        mainWindow.SetActive(false);
        levelSelectionWindow.SetActive(true);
    }

    public void SwitchToMain()
    {
        mainWindow.SetActive(true);
        levelSelectionWindow.SetActive(false);
    }

    public void StartGame()
    {
        Gamemaster.Instance.SetNextMainLevelToLoad(0);
        LoadScene(SceneDictionary.Play);
    }

    public void GoToEditor()
    {
        LoadScene(SceneDictionary.LevelEditor);

    }

    public void EndGame()
    {
        Application.Quit();
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
