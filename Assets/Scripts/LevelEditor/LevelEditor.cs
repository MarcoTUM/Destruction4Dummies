using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]
    private EditorInput editorInput;

    private Block_Data[] blockDatas = new Block_Data[6] {   new StartBlock_Data(),
                                                            new GoalBlock_Data(),
                                                            new EmptyBlock_Data(),
                                                            new WoodBlock_Data(),
                                                            new StoneBlock_Data(),
                                                            new ChainBlock_Data() };

    private Block_Data currentBlockData = new EmptyBlock_Data();
    private Level_Data data;

    private const int LEVEL_WIDTH = 100;
    private const int LEVEL_HEIGHT = 50;

    #region Unity
    private void Awake()
    {
        Gamemaster.Instance.Register(this);
    }

    void Start()
    {
        Gamemaster.Instance.GetLevel().CreateNewLevel(LEVEL_WIDTH, LEVEL_HEIGHT, "testLevel");
        data = Gamemaster.Instance.GetLevel().GetLevelData();
    }

    private void Update()
    {
        // Check if the mouse was clicked over a UI element
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2Int blockCoord = editorInput.GetBlockMouseIsOn();
            if (blockCoord.x >= 0 && blockCoord.y >= 0)
            {
                Gamemaster.Instance.GetLevel().PlaceBlock(blockCoord.x, blockCoord.y, currentBlockData);
            }
        }
    }

    #endregion

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
        data.Name = levelName;
    }

    public void SetLevelName(InputField levelNameInputField)
    {
        data.Name = levelNameInputField.text;
    }

    public void SaveLevel()
    {
        LevelSaveLoad.Save(data, FilePaths.CustomLevelFolder);
    }

    public void LoadLevel()
    {
        Gamemaster.Instance.GetLevel().LoadLevelFromFile(data.Name, FilePaths.CustomLevelFolder);
        data = Gamemaster.Instance.GetLevel().GetLevelData();
    }

    public void ExitLevelEditor()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
