using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaySceneUI : MonoBehaviour
{
    [SerializeField] private float windowResizeTime = 1f;
    [SerializeField] private RectTransform mainLevelCompleteWindow;
    [SerializeField] private RectTransform customLevelCompleteWindow;
    private void Awake()
    {
        Gamemaster.Instance.Register(this);
    }

    #region LevelCompleteWindow
    public void OpenLevelCompleteWindow()
    {
        if(Gamemaster.Instance.GetLevelType() == LevelType.Main)
            StartCoroutine(ResizeWindow(mainLevelCompleteWindow, Vector3.zero, Vector3.one));
        else if (Gamemaster.Instance.GetLevelType() == LevelType.Custom)
            StartCoroutine(ResizeWindow(customLevelCompleteWindow, Vector3.zero, Vector3.one));

    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneDictionary.MainMenu);
    }

    public void NextLevel()
    {
        Gamemaster.Instance.SetNextMainLevelToLoad();
        SceneManager.LoadScene(SceneDictionary.Play);
    }

    #endregion

    #region Helper
    private IEnumerator ResizeWindow(RectTransform window, Vector3 startScale, Vector3 goalScale)
    {
        Button[] buttons = window.GetComponentsInChildren<Button>();
        EnDisableButtons(buttons, false);
        window.gameObject.SetActive(true);
        float timer = 0;
        window.localScale = startScale;
        while(timer < windowResizeTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            window.localScale = Vector3.Lerp(startScale, goalScale, timer / windowResizeTime);
        }
        EnDisableButtons(buttons, true);
        window.localScale = goalScale;
        if (goalScale.x == 0 || goalScale.y == 0 || goalScale.z == 0)
            window.gameObject.SetActive(false);

    }

    private void EnDisableButtons(Button[] buttons, bool enable)
    {
        foreach(Button button in buttons)
        {
            button.interactable = enable;
        }
    }
    #endregion
}
