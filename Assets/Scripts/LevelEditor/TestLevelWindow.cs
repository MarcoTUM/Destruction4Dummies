using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestLevelWindow : MonoBehaviour
{
    [SerializeField] private Button exportButton;

    private void OnEnable()
    {
        exportButton.interactable = Gamemaster.Instance.GetLevel().GetLevelData().IsExportable;
    }

    public void Open()
    {
        this.gameObject.SetActive(true);
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    public void ExportLevel()
    {
        LevelSaveLoad.Save(Gamemaster.Instance.GetLevel().GetLevelData(), FilePaths.CustomPlayLevelFolder);
    }

    public void TestLevel()
    {
        Gamemaster.Instance.GetLevelEditor().SaveLevel();
        Gamemaster.Instance.SetNextTestLevelToLoad();
        SceneManager.LoadScene(SceneDictionary.Play);
    }
}
