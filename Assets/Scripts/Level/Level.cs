using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelType { Main, Custom };
public class Level : MonoBehaviour
{
    private const int platformWidth = 3;
    [SerializeField] private GameObject[] blockPrefabs;
    GameObject currentLevel;
    private GameObject[,] blockMap; //Contains all the Block gameObjects in the current level
    private Level_Data levelData; //Contains all the information used to save and load the levels
    private int width, height;
    #region Unity
    private void Awake()
    {
        Gamemaster.Instance.Register(this);
    }
    #endregion

    #region LevelCreation
    /// <summary>
    /// Create a new Level from scratch
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="name"></param>
    public void CreateNewLevel(int width, int height, string name)
    {
        this.width = width;
        this.height = height;
        levelData = new Level_Data(width, height, name);
        CreateLevel();
        LevelSaveLoad.Save(this.levelData, FilePaths.CustomLevelFolder);
    }

    /// <summary>
    /// Load levelData from fileSystem and create the level
    /// </summary>
    /// <param name="levelName">Name of Level</param>
    /// <param name="subFolder">FolderName e.g. Custom</param>
    public void LoadLevelFromFile(string levelName, string directoryPath)
    {
        Level_Data loadResult = LevelSaveLoad.Load(levelName, directoryPath);
        if (loadResult != null)
            levelData = loadResult;
        else
            return;
        this.width = levelData.BlockMap.GetLength(0);
        this.height = levelData.BlockMap.GetLength(1);
        CreateLevel();
    }

    /// <summary>
    /// Creates levelObjects and initializes them with the data in levelData
    /// </summary>
    private void CreateLevel()
    {
        bool deactivateEmptyBlocks = SceneManager.GetActiveScene().name != SceneDictionary.LevelEditor;
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        currentLevel = new GameObject();
        currentLevel.name = levelData.Name;
        currentLevel.transform.SetParent(this.transform);
        blockMap = new GameObject[width, height];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {

                Block_Data blockData = levelData.BlockMap[j, i];
                GameObject blockObject = Instantiate(blockPrefabs[(int)blockData.BlockType]);
                blockObject.name = "[" + j + "-" + i + "]";
                blockObject.transform.SetParent(currentLevel.transform);
                blockObject.transform.localPosition = new Vector3(j * Block_Data.BlockSize, i * Block_Data.BlockSize, 0);
                blockMap[j, i] = blockObject;
                blockObject.GetComponent<Block>().InitializeBlock(blockData);
                if (deactivateEmptyBlocks && blockData.BlockType == BlockType.Empty)
                {
                    blockObject.SetActive(false);
                }
            }
        }
    }
    #endregion

    #region LevelEditing

    /// <summary>
    /// Removes oldStartPlatform -> unlocks old locked Blocks -> Sets new startPlatform -> locks blocks above it
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetStartPlatform(int x, int y)
    {
        x = Mathf.Clamp(x, platformWidth / 2, width - (platformWidth + 1) / 2);

        int oldX = levelData.StartPlatformCoordinates.x;
        int oldY = levelData.StartPlatformCoordinates.y;

        int oldlockedHeight = Mathf.Min(height - oldY, 3);
        int lockedHeight = Mathf.Min(height - y, 3);
        //Remove old StartPlatform
        EmptyBlock_Data emptyData = new EmptyBlock_Data();
        for (int i = 0; i < platformWidth; i++)
        {
            SetBlock((oldX - platformWidth / 2 + i), oldY, emptyData);
        }
        //Release lock on blocks above StartPlatform
        for (int j = 1; j < oldlockedHeight; j++)
        {
            for (int i = 0; i < platformWidth; i++)
            {
                levelData.BlockMap[oldX - platformWidth / 2 + i, Mathf.Min(oldY + j, height - 1)].IsReplaceable = true;
            }
        }

        EmptyBlock_Data lockedEmptyData = new EmptyBlock_Data();
        lockedEmptyData.IsReplaceable = false;
        //Place new StartPlatform
        StartBlock_Data startData = new StartBlock_Data();
        for (int i = 0; i < platformWidth; i++)
        {
            SetBlock((x - platformWidth / 2 + i), y, startData);
        }
        //Lock blocks above startPlatform(empty or otherwise player stuck in block)
        for (int j = 1; j < lockedHeight; j++)
        {
            for (int i = 0; i < platformWidth; i++)
            {
                SetBlock((x - platformWidth / 2 + i), Mathf.Min(y + j, height - 1), lockedEmptyData);
            }
        }

        levelData.StartPlatformCoordinates = new Vector2Int(x, y);
    }

    public void SetGoalPlatform(int x, int y)
    {
        x = Mathf.Clamp(x, platformWidth / 2, width - (platformWidth + 1) / 2);

        int oldX = levelData.GoalPlatformCoordinates.x;
        int oldY = levelData.GoalPlatformCoordinates.y;
        //Remove old GoalPlatform
        EmptyBlock_Data emptyData = new EmptyBlock_Data();
        for (int i = 0; i < platformWidth; i++)
        {
            SetBlock((oldX - platformWidth / 2 + i), oldY, emptyData);
        }

        //Place new GoalPlatform
        GoalBlock_Data data = new GoalBlock_Data();
        for (int i = 0; i < platformWidth; i++)
        {
            SetBlock((x - platformWidth / 2 + i), y, data);
        }

        levelData.GoalPlatformCoordinates = new Vector2Int(x, y);
    }

    /// <summary>
    /// Try placing a Block at the position (x,y) 
    /// Filters platforms and normal blocks
    /// Can fail if action currently not allowed
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="data"></param>
    public bool PlaceBlock(int x, int y, Block_Data data)
    {
        if (data.BlockType == BlockType.Start)
        {
            if (levelData.StartPlatformCoordinates.x == x && levelData.StartPlatformCoordinates.y == y)
                return false;
            if (!CanStartPlatformBePlaced(x, y))
                return false;
            SetStartPlatform(x, y);
        }
        else if (data.BlockType == BlockType.Goal)
        {
            if (levelData.GoalPlatformCoordinates.x == x && levelData.GoalPlatformCoordinates.y == y)
                return false;
            if (!CanEndPlatformBePlaced(x, y))
                return false;
            SetGoalPlatform(x, y);
        }
        else
        {
            BlockType oldBlockType = levelData.BlockMap[x, y].BlockType;
            if (!levelData.BlockMap[x, y].IsReplaceable) //not allowed to replace certain blocks
                return false;
            if (levelData.BlockMap[x, y].Equals(data)) //no point in replacing block with themselves
                return false;
            SetBlock(x, y, data);
        }

        return true;
    }

    private bool CanEndPlatformBePlaced(int x, int y)
    {
        for (int i = 0; i < platformWidth; i++)
        {
            if (!levelData.BlockMap[Mathf.Clamp(x + i - platformWidth / 2, 0, width - 1), y].IsReplaceable)
                return false;
        }
        return true;
    }

    private bool CanStartPlatformBePlaced(int x, int y)
    {
        int lockedHeight = Mathf.Min(height - y, 3);
        for (int j = 0; j < lockedHeight; j++)
        {
            for (int i = 0; i < platformWidth; i++)
            {
                if (levelData.BlockMap[Mathf.Clamp(x + i - platformWidth / 2, 0, width - 1), y + j].BlockType == BlockType.Goal)
                    return false;
            }
        }
        return true;
    }

    private void SetBlock(int x, int y, Block_Data data)
    {
        GameObject newBlockObject = Instantiate(blockPrefabs[(int)data.BlockType]);
        newBlockObject.transform.position = blockMap[x, y].transform.position;
        Destroy(blockMap[x, y]);//Could lead to performance problems
        blockMap[x, y] = newBlockObject;
        newBlockObject.name = "[" + x + "-" + y + "]";
        newBlockObject.transform.SetParent(currentLevel.transform);
        Block newBlock = newBlockObject.GetComponent<Block>();
        newBlock.InitializeBlock(data);
        levelData.BlockMap[x, y] = data;
    }

    /// <summary>
    /// Gets width of full levelObject in unity units
    /// </summary>
    /// <returns></returns>
    public int GetWorldWidth()
    {
        return width * Block_Data.BlockSize;
    }

    /// <summary>
    /// Gets height of full levelObject in unity units
    /// </summary>
    /// <returns></returns>
    public int GetWorldHeight()
    {
        return height * Block_Data.BlockSize;
    }

    public void ShowEmptyBlocks(bool show)
    {
        foreach(GameObject obj in blockMap)
        {
            if(obj.GetComponent<Block>().BlockData.BlockType == BlockType.Empty)
            {
                obj.SetActive(show);
            }
        }
    }

    #endregion

    #region PlayLevel
    public Vector2Int GetLevelDimensions()
    {
        return new Vector2Int(width, height);
    }

    public void ResetLevel()
    {
        foreach (GameObject block in blockMap)
        {
            block.GetComponent<Block>().ResetBlock();//maybe better to change blockmap to Block?
        }
    }
    #endregion

    #region DebugMethods
    public Level_Data GetLevelData()
    {
        return levelData;
    }
    #endregion

}
