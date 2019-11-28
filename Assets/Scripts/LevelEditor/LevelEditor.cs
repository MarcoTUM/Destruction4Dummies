using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks if Mous is clicking on a block on the grid and places new block if necessary
/// Handles change of currentBlock + Saving etc.
/// </summary>
/// 
public class LevelEditor : MonoBehaviour
{
    [SerializeField] private EditorInput editorInput;
    [SerializeField] private Level level;
    Block_Data[] blockDatas = new Block_Data[4] { new StartBlock_Data(), new GoalBlock_Data(), new EmptyBlock_Data(), new WoodBlock_Data() };
    
    private Block_Data currentBlockData = new EmptyBlock_Data();

    #region Unity
    void Update()
    {
        //toDo: Replace simple KeyInput
        for (int i = 0; i < 4; i++) {
            if (Input.GetKeyDown("" + (i+1)))
            {
                currentBlockData = blockDatas[i];
                break;
            }
        }

        Vector2Int blockCoord = editorInput.GetBlockMouseIsOn();
        if (blockCoord.x >= 0 && blockCoord.y >= 0)
        {
            level.PlaceBlock(blockCoord.x, blockCoord.y, currentBlockData);
        }
    }

    #endregion

    public void SetCurrentBlock(Block_Data blockData)
    {
        currentBlockData = blockData;
    }
}
