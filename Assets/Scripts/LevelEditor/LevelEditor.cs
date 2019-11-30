using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Checks if Mous is clicking on a block on the grid and places new block if necessary
/// Handles change of currentBlock + Saving etc.
/// </summary>
/// 
public class LevelEditor : MonoBehaviour
{
    [SerializeField] private EditorInput editorInput;
    Block_Data[] blockDatas = new Block_Data[4] { new StartBlock_Data(), new GoalBlock_Data(), new EmptyBlock_Data(), new WoodBlock_Data() };

    private Block_Data currentBlockData = new EmptyBlock_Data();

    private Level_Data data;

    #region Unity
    private void Awake()
    {
        Gamemaster.Instance.Register(this);
    }

    void Start()
    {
        Gamemaster.Instance.GetLevel().CreateNewLevel(20, 20, "testLevel");
        data = Gamemaster.Instance.GetLevel().GetLevelData();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
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
        if (blockType >= 0 && blockType < 4)
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
        LevelSaveLoad.Save(data, "TestLevels");
    }

    public void LoadLevel()
    {
        Gamemaster.Instance.GetLevel().LoadLevelFromFile(data.Name, "TestLevels");
    }
}
