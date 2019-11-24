using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        Block defaultBlock = blockPrefabs[(int)BlockType.EmptyBlock].GetComponent<EmptyBlock>();
        levelData = new Level_Data(width, height, name, defaultBlock.BlockData);
        CreateLevel();
    }

    /// <summary>
    /// Load levelData from fileSystem and create the level
    /// </summary>
    /// <param name="levelName">Name of Level</param>
    /// <param name="subFolder">FolderName e.g. Custom</param>
    public void LoadLevelFromFile(string levelName, string subFolder)
    {
        levelData = LevelSaveLoad.Load(levelName, subFolder);
        this.width = levelData.BlockMap.GetLength(0);
        this.height = levelData.BlockMap.GetLength(1);
        CreateLevel();
    }

    /// <summary>
    /// Creates levelObjects and initializes them with the data in levelData
    /// </summary>
    private void CreateLevel()
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel);
        }
        currentLevel = new GameObject();
        currentLevel.name = levelData.Name;
        currentLevel.transform.SetParent(this.transform);
        this.transform.position = new Vector3(-height * Block_Data.BlockSize / 2f, -width * Block_Data.BlockSize / 2f, 0); //To do maybe change posiiton of level
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
            }
        }
    }
    #endregion

    #region LevelEditing
    public void SetBlock(int x, int y, BlockType blockType)
    {
        GameObject newBlock = Instantiate(blockPrefabs[(int)blockType]);
        newBlock.transform.position = blockMap[x, y].transform.position;
        Destroy(blockMap[x, y]);//Could lead to performance problems
        blockMap[x, y] = newBlock;
        levelData.BlockMap[x, y] = newBlock.GetComponent<Block>().BlockData;
    }
    #endregion
    private void OnApplicationQuit()
    {
        LevelSaveLoad.Save(levelData, "TestLevels");
    }
}

[System.Serializable]
public class Level_Data
{
    public string Name;
    public Block_Data[,] BlockMap;

    public Level_Data(int width, int height, string name, Block_Data defaultBlock)
    {
        this.Name = name;
        this.BlockMap = new Block_Data[width, height];
        
        for(int i=0; i<height; i++)
        {
            for(int j=0; j<width; j++)
            {
                BlockMap[j, i] = defaultBlock;
            }
        }
    }
}
