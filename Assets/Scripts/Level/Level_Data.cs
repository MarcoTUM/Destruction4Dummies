using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Vector2Int
{
    public int x, y;
    public Vector2Int(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return "(" + x + "," + y + ")";
    }
}

[System.Serializable]
public class Level_Data
{
    public bool IsExportable;
    public string Name;
    public const int MinDimension = 7;
    public Block_Data[,] BlockMap;
    [SerializeField]
    public Vector2Int StartPlatformCoordinates, GoalPlatformCoordinates;
    /// <summary>
    /// Creates a new Level filled with Empty blocks and a start and endPlatform
    /// </summary>
    /// <param name="width">must be at least 7</param>
    /// <param name="height">must be at least 7</param>
    /// <param name="name"></param>
    public Level_Data(int width, int height, string name)
    {
        if (width < MinDimension)
            throw new InvalidOperationException($"Trying to create new {nameof(Level_Data)} with width of " + width + " but it must be at least " + MinDimension + "!");
        if (height < MinDimension)
            throw new InvalidOperationException($"Trying to create new {nameof(Level_Data)} with height of " + height + " but it must be at least " + MinDimension + "!");

        this.Name = name;
        this.BlockMap = new Block_Data[width, height];
        StartPlatformCoordinates = new Vector2Int(1, height / 2);
        GoalPlatformCoordinates = new Vector2Int(width - 2, height / 2);
        Block_Data emptyBlock = new EmptyBlock_Data();
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (i == height / 2)
                {
                    if (j < 3)
                    {
                        BlockMap[j, i] = new StartBlock_Data();
                    }
                    else if (j > width - 4)
                        BlockMap[j, i] = new GoalBlock_Data();
                    else
                        BlockMap[j, i] = emptyBlock;
                }
                else
                {
                    BlockMap[j, i] = emptyBlock;
                }

            }
        }
        Block_Data lockedEmptyBlock = new EmptyBlock_Data();
        lockedEmptyBlock.IsReplaceable = false;
        for (int j = 1; j < 3; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                BlockMap[i, j + height / 2] = lockedEmptyBlock;
            }
        }
    }
}
