using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelType { Main, Custom };
public class Level : MonoBehaviour
{
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
    }

    /// <summary>
    /// Load levelData from fileSystem and create the level
    /// </summary>
    /// <param name="levelName">Name of Level</param>
    /// <param name="subFolder">FolderName e.g. Custom</param>
    public void LoadLevelFromFile(string levelName, string directoryPath)
    {
        levelData = LevelSaveLoad.Load(levelName, directoryPath);
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
        //this.transform.position = new Vector3(-height * Block_Data.BlockSize / 2f, -width * Block_Data.BlockSize / 2f, 0); //To do maybe change posiiton of level
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

    public void SetStartPlatform(int x, int y)
    {
        if (x == 0) x++;
        else if (x == width - 1) x--;

        int oldX = levelData.StartPlatformCoordinates.x;
        int oldY = levelData.StartPlatformCoordinates.y;

        //Remove old StartPlatform
        EmptyBlock_Data emptyData = new EmptyBlock_Data();
        for (int i = 0; i < 3; i++)
        {
            SetBlock((oldX - 1 + i), oldY, emptyData);
        }

        //Place new StartPlatform
        StartBlock_Data startData = new StartBlock_Data();
        for (int i = 0; i < 3; i++)
        {
            SetBlock((x - 1 + i), y, startData);
        }
        levelData.StartPlatformCoordinates = new Vector2Int(x, y);
    }

    public void SetGoalPlatform(int x, int y)
    {
        if (x == 0) x++;
        else if (x == width - 1) x--;

        int oldX = levelData.GoalPlatformCoordinates.x;
        int oldY = levelData.GoalPlatformCoordinates.y;
        //Remove old GoalPlatform
        EmptyBlock_Data emptyData = new EmptyBlock_Data();
        for (int i = 0; i < 3; i++)
        {
            SetBlock((oldX - 1 + i), oldY, emptyData);
        }

        //Place new GoalPlatform
        GoalBlock_Data data = new GoalBlock_Data();
        for (int i = 0; i < 3; i++)
        {
            SetBlock((x - 1 + i), y, data);
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
            if (CollidesWithPlatform(levelData.GoalPlatformCoordinates, x, y))
                return false;
            SetStartPlatform(x, y);
        }
        else if (data.BlockType == BlockType.Goal)
        {
            if (levelData.GoalPlatformCoordinates.x == x && levelData.GoalPlatformCoordinates.y == y)
                return false;
            if (CollidesWithPlatform(levelData.StartPlatformCoordinates, x, y))
                return false;
            SetGoalPlatform(x, y);
        }
        else
        {
            BlockType oldBlockType = levelData.BlockMap[x, y].BlockType;
            if (oldBlockType == BlockType.Start || oldBlockType == BlockType.Goal) //not allowed to replace start/goalBlocks
                return false;
            if (oldBlockType == data.BlockType) //no point in replacing block with themselves
                return false;

            SetBlock(x, y, data);
        }

        return true;
    }

    private bool CollidesWithPlatform(Vector2Int platformCoord, int x, int y)
    {
        return platformCoord.y == y && platformCoord.x - 2 <= x && platformCoord.x + 2 >= x;
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

    #endregion

    #region PlayLevel
    public Vector2Int GetLevelDimensions()
    {
        return new Vector2Int(width, height);
    }

    public void ResetLevel()
    {
        foreach(GameObject block in blockMap)
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
