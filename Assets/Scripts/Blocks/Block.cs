using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType { EmptyBlock = 0, WoodBlock = 1 };

public abstract class Block : MonoBehaviour
{
    public abstract Block_Data BlockData { get; set; }

    public BlockType GetBockType()
    {
        return BlockData.BlockType;
    }

    /// <summary>
    /// Sets the BlockData of a block and updates the block
    /// </summary>
    /// <param name="data"></param>
    public virtual void InitializeBlock(Block_Data data)
    {
        BlockData = data;
    }

    /// <summary>
    /// Handles interaction when player touches the block
    /// </summary>
    protected abstract void OnTouch();

    /// <summary>
    /// Sets the block to its original state
    /// </summary>
    public abstract void ResetBlock();
    
    #region Unity
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == TagDictionary.Player)
        {
            OnTouch();
        }
    }
    #endregion
}
