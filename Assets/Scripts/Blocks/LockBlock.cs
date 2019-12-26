using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBlock : Block
{
    private Block_Data lockBlockData = new LockBlock_Data();
    public override Block_Data BlockData { get => lockBlockData; set => lockBlockData = value; }

    [SerializeField]
    private uint blockID = 0;

    [SerializeField]
    private Material unlockMaterial;

    private Material lockMaterial;

    #region Initialization / Destruction
    public override void InitializeBlock(Block_Data data)
    {
        base.InitializeBlock(data);
        this.blockID = ((LockBlock_Data)data).GetID();
        this.GetComponent<Renderer>().material.color = LockBlock_Data.LockBlockColors[this.blockID - 1];

        // Save current material for later reset
        lockMaterial = gameObject.GetComponent<Renderer>().material;
    }

    protected override void DestroyBlock()
    {
        base.DestroyBlock();
    }

    public override void ResetBlock()
    {
        // Relock block
        ((LockBlock_Data)BlockData).SetLock(true);
        gameObject.GetComponent<Renderer>().material = lockMaterial;

        base.ResetBlock();
    }


    #endregion

    #region PlayerInteraction

    protected override void OnTouch(GameObject player)
    {
        // If the the lockBlock is unlocked
        if (!((LockBlock_Data)BlockData).GetLock())
        {
            // Destroy the lockBlock
            base.OnTouch(player);
        }
    }

    protected override void OnTouchEnd(GameObject player)
    {
        base.OnTouchEnd(player);
    }

    #endregion

    public void UnlockBlock()
    {
        ((LockBlock_Data)BlockData).SetLock(false);
        Color tempColor = this.GetComponent<Renderer>().material.color;
        gameObject.GetComponent<Renderer>().material = unlockMaterial;
        this.GetComponent<Renderer>().material.color = tempColor;
    }
}