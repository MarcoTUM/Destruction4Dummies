using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaySceneUI : MonoBehaviour
{
    [SerializeField] private float windowResizeTime = 0.4f;
    [SerializeField] private float pauseWindowResizeTime = 0.2f;
    [SerializeField] private RectTransform mainLevelCompleteWindow;
    [SerializeField] private RectTransform customLevelCompleteWindow;
    [SerializeField] private RectTransform testLevelCompleteWindow;
    [SerializeField] private RectTransform pausedWindow;
    [SerializeField] private RectTransform pausedEditorWindow;
    [SerializeField] private GameObject exportedText;
    public bool IsOpen { private set; get; }

    private void Awake()
    {
        IsOpen = false;
        Gamemaster.Instance.Register(this);
    }

    public void OpenPauseWindow()
    {
        if(Gamemaster.Instance.GetLevelType() == LevelType.Test)
            StartCoroutine(ResizeWindow(pausedEditorWindow, Vector3.zero, Vector3.one, pauseWindowResizeTime));
        else
            StartCoroutine(ResizeWindow(pausedWindow, Vector3.zero, Vector3.one, pauseWindowResizeTime));
    }

    public void ClosePauseWindow()
    {
         if(Gamemaster.Instance.GetLevelType() == LevelType.Test)
            StartCoroutine(ResizeWindow(pausedEditorWindow, Vector3.one, Vector3.zero, pauseWindowResizeTime));
        else
            StartCoroutine(ResizeWindow(pausedWindow, Vector3.one, Vector3.zero, pauseWindowResizeTime));
    }
    #region LevelCompleteWindow
    public void OpenLevelCompleteWindow()
    {
        LevelType type = Gamemaster.Instance.GetLevelType();
        switch (type)
        {
            case LevelType.Main:
                if(Gamemaster.Instance.HasNextLevel())
                    StartCoroutine(ResizeWindow(mainLevelCompleteWindow, Vector3.zero, Vector3.one, windowResizeTime));
                else
                    StartCoroutine(ResizeWindow(customLevelCompleteWindow, Vector3.zero, Vector3.one, windowResizeTime));
                break;
            case LevelType.Custom:
                StartCoroutine(ResizeWindow(customLevelCompleteWindow, Vector3.zero, Vector3.one, windowResizeTime));
                break;
            case LevelType.Test:
                StartCoroutine(ResizeWindow(testLevelCompleteWindow, Vector3.zero, Vector3.one, windowResizeTime));
                break;
            default:
                throw new InvalidOperationException("Leveltype case not defined for: " + type);
        }
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneDictionary.MainMenu);
    }

    public void NextLevel()
    {
        Gamemaster.Instance.SetNextMainLevelToLoad();
        SceneManager.LoadScene(SceneDictionary.Play);
    }

    public void BackToEditor()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneDictionary.LevelEditor); 
    }

    public void ExportLevel()
    {
        LevelSaveLoad.Save(Gamemaster.Instance.GetLevel().GetLevelData(), FilePaths.CustomPlayLevelFolder);
        exportedText.SetActive(true);
    }

    #endregion

    #region Helper
    private IEnumerator ResizeWindow(RectTransform window, Vector3 startScale, Vector3 goalScale, float resizeTime)
    {
        Button[] buttons = window.GetComponentsInChildren<Button>();
        EnDisableButtons(buttons, false);
        window.gameObject.SetActive(true);
        float timer = 0;
        window.localScale = startScale;
        while (timer < resizeTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.unscaledDeltaTime;
            window.localScale = Vector3.Lerp(startScale, goalScale, timer / resizeTime);
        }
        EnDisableButtons(buttons, true);
        window.localScale = goalScale;
        IsOpen = goalScale.x != 0 && goalScale.y != 0 && goalScale.z != 0;
        window.gameObject.SetActive(IsOpen);

    }

    private void EnDisableButtons(Button[] buttons, bool enable)
    {
        foreach (Button button in buttons)
        {
            button.interactable = enable;
        }
    }
    #endregion
}
