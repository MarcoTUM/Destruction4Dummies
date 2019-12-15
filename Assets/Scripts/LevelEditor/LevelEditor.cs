using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Checks if Mous is clicking on a block on the grid and places new block if necessary
/// Handles change of currentBlock + Saving etc.
/// </summary>
/// 
public class LevelEditor : MonoBehaviour
{
    [SerializeField] private EditorInput editorInput;
    [SerializeField] private InputField levelNameInputField;
    [SerializeField] private LevelEditorMenu menu;
    private Block_Data[] blockDatas = new Block_Data[6] {   new StartBlock_Data(),
                                                            new GoalBlock_Data(),
                                                            new EmptyBlock_Data(),
                                                            new WoodBlock_Data(),
                                                            new StoneBlock_Data(),
                                                            new ChainBlock_Data() };

    private Block_Data currentBlockData = new EmptyBlock_Data();
    private Level_Data data;
    

    #region Unity
    private void Awake()
    {
        Gamemaster.Instance.Register(this);
    }

    private void Update()
    {
        // Check if the mouse was clicked over a UI element
        if (Input.GetMouseButton(InputDictionary.MouseLeftClick) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2Int blockCoord = editorInput.GetBlockMouseIsOn();
            if (blockCoord.x >= 0 && blockCoord.y >= 0)
            {
                Gamemaster.Instance.GetLevel().PlaceBlock(blockCoord.x, blockCoord.y, currentBlockData);
            }
        }
    }

    #endregion

    /// <summary>
    /// Initializes Camera and sets correct levelData + name
    /// </summary>
    /// <param name="name"></param>
    public void BeginEditLevel(string name)
    {
        if (name != "")
            Gamemaster.Instance.GetLevel().LoadLevelFromFile(name, FilePaths.CustomLevelFolder);
        Camera.main.GetComponent<LevelEditorCamera>().InitializeCamera();
        data = Gamemaster.Instance.GetLevel().GetLevelData();
        levelNameInputField.text = data.Name;
    }

    public void SetCurrentBlock(Block_Data blockData)
    {
        currentBlockData = blockData;
    }

    public void SetBlockType(int blockType)
    {
        if (blockType >= 0 && blockType < blockDatas.Length)
        {
            currentBlockData = blockDatas[blockType];
        }
    }

    public void SetLevelName(string levelName)
    {
        if (menu.NameAlreadyExists(levelName))
        {
            levelNameInputField.text = data.Name;
            Debug.Log("Name already exists");
            return;
        }

        string oldName = data.Name;
        data.Name = levelName;
        LevelSaveLoad.Rename(oldName, data.Name, FilePaths.CustomLevelFolder);
        SaveLevel();
        menu.UpdateButtonName(oldName, data.Name);
    }

    public void SaveLevel()
    {
        LevelSaveLoad.Save(data, FilePaths.CustomLevelFolder);
    }

    /*
     * //Can be removed i guess - if we load with menu
    public void LoadLevel()
    {
        string path = EditorUtility.OpenFilePanel("Choose Level", FilePaths.CustomLevelFolder, "dat");
        int splitIndex = path.LastIndexOf('/');
        if (splitIndex == -1)
            return;
        string directoryPath = path.Substring(0, splitIndex + 1);
        string fileName = path.Substring(splitIndex + 1);
        if (directoryPath != FilePaths.CustomLevelFolder)
            return;
        fileName = fileName.Replace(".dat", "");
        levelNameInputField.text = fileName;
        Gamemaster.Instance.GetLevel().LoadLevelFromFile(fileName, FilePaths.CustomLevelFolder);
        data = Gamemaster.Instance.GetLevel().GetLevelData();
    }
    */
    public void BackToMenu()
    {
        SaveLevel();
        menu.OpenEditorMenu();
        //SceneManager.LoadScene("MainMenu");
    }

}
