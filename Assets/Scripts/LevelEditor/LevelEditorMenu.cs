using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEditorMenu : LevelButtonGroup
{
    [SerializeField] private GameObject newLevelWindow, menuObject;
    [SerializeField] private InputField nameInput, widthInput, heightInput;
    private List<string> fileNames; //all existing names
    private List<LevelEditorLevelButton> buttons; //all buttons
    private LevelEditorLevelButton selectedButton; //currently selected button in edit Mode

    protected override void Awake()
    {
        buttons = new List<LevelEditorLevelButton>();
        if (buttonPrefab.GetComponent<LevelEditorLevelButton>() == null)
            throw new InvalidOperationException($"ButtonPrefab does not have {nameof(LevelEditorLevelButton)}");
        TestDirectory(FilePaths.CustomEditLevelFolder);
        fileNames = Directory.GetFiles(FilePaths.CustomEditLevelFolder).
            Where(filePath => filePath.EndsWith(".dat")). //ignore meta files
            OrderBy(filePath => new FileInfo(filePath).CreationTime). // sort fileNames by id 
            Select(filePath => ConvertFilePathToName(filePath)).ToList<string>(); //cut filePath to fileName


        levelCount = fileNames.Count;
        SetHeight();
        SpawnButtons(fileNames.ToArray());
    }

    private void Start()
    {
        if (Gamemaster.Instance.GetLevelType() == LevelType.Test)
        {
            Gamemaster.Instance.SetDefaultLevelToLoad();
            ContinueEdit();
        }
    }

    protected override void SpawnButtons(string[] fileNames)
    {
        for (int i = 0; i < levelCount; i++)
        {
            buttons.Add(SpawnButton(fileNames[i]));
        }
    }

    protected LevelEditorLevelButton SpawnButton(string name)
    {
        GameObject buttonObject = InstantiateButtonAsChild();
        LevelEditorLevelButton button = buttonObject.GetComponent<LevelEditorLevelButton>();
        button.SetLevelName(name);
        button.SetLevelEditorMenu(this);
        return button;
    }

    #region NewLevel
    /// <summary>
    /// Opens newLevel Window
    /// </summary>
    public void NewLevel()
    {
        newLevelWindow.SetActive(true);
    }
    /// <summary>
    /// Checks if values in Inputsfields are correct => Creates new level and starts edit mode with it
    /// </summary>
    public void CreateNewLevel()
    {
        string name = nameInput.text;
        int width, height;
        //Test if inputFields are correctly set
        if (name == "" || fileNames.Contains(name))
        {
            Debug.Log("FileName is null or already exists");
            return;
        }
        if (!int.TryParse(widthInput.text, out width) || !int.TryParse(heightInput.text, out height))
        {
            Debug.Log("width or heightInput field empty");
            return;
        }
        if (width < Level_Data.MinDimension || height < Level_Data.MinDimension)
        {
            Debug.Log("width or height too small");
            return;
        }

        //Creates new LevelData
        fileNames.Add(name);
        selectedButton = SpawnButton(name);
        buttons.Add(selectedButton);
        Gamemaster.Instance.GetLevel().CreateNewLevel(width, height, name);

        //Go into Edit mode
        CloseNewLevelWindow();
        menuObject.SetActive(false);
        Gamemaster.Instance.GetLevelEditor().BeginEditLevel("");
    }

    public void CloseNewLevelWindow()
    {
        nameInput.text = "";
        heightInput.text = "";
        widthInput.text = "";
        newLevelWindow.SetActive(false);

    }

    #endregion

    #region EditLevel
    /// <summary>
    /// Closes EditorMenu and goes into Edit mode
    /// </summary>
    /// <param name="button"></param>
    public void ChooseLevel(LevelEditorLevelButton button)
    {
        selectedButton = button;
        menuObject.SetActive(false);
        Gamemaster.Instance.GetLevelEditor().BeginEditLevel(button.GetName());
    }

    /// <summary>
    /// Test if file with Name already exists
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool NameAlreadyExists(string name)
    {
        return fileNames.Contains(name);
    }

    /// <summary>
    /// Updates name of button + fileNameList
    /// Called when user changes name in Edit mode
    /// </summary>
    /// <param name="oldName"></param>
    /// <param name="newName"></param>
    public void UpdateButtonName(string oldName, string newName)
    {
        selectedButton.SetLevelName(newName);
        fileNames.Remove(oldName);
        fileNames.Add(newName);
    }

    public void OpenEditorMenu()
    {
        menuObject.SetActive(true);
    }

    public void ContinueEdit()
    {
        string name = Gamemaster.Instance.GetLevel().GetLevelData().Name;
        selectedButton = buttons.Where(button => button.GetName() == name).FirstOrDefault();
        menuObject.SetActive(false);
        Gamemaster.Instance.GetLevelEditor().BeginEditLevel("");
    }
    #endregion

    #region DeleteLevel
    public void SwitchDeleteMode()
    {
        foreach (LevelEditorLevelButton button in buttons)
        {
            button.SwitchDeleteMode();
        }
    }

    /// <summary>
    /// Delete saveFile for button, removes it from buttonList and nameList + destroysButtonObject
    /// </summary>
    /// <param name="button"></param>
    public void DeleteLevel(LevelEditorLevelButton button)
    {
        LevelSaveLoad.Delete(button.GetName(), FilePaths.CustomEditLevelFolder);
        buttons.Remove(button);
        fileNames.Remove(button.GetName());
        Destroy(button.gameObject);
    }
    #endregion


    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
