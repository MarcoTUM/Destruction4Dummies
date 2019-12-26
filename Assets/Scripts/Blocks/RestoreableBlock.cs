using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestoreableBlock : Block
{
    private Block_Data restoreableBlockData = new RestoreableBlock_Data();
    public override Block_Data BlockData { get => restoreableBlockData; set => restoreableBlockData = value; }

    [SerializeField]
    private uint blockID = 0;

    [SerializeField]
    private Material restoredMaterial;

    private Material restoreableMaterial;

    #region Initialization / Destruction
    public override void InitializeBlock(Block_Data data)
    {
        gameObject.GetComponent<Collider>().enabled = SceneManager.GetActiveScene().name != SceneDictionary.Play;

        base.InitializeBlock(data);
        this.blockID = ((RestoreableBlock_Data)data).GetID();
        this.GetComponent<Renderer>().material.color = RestoreableBlock_Data.RestoreableBlockColors[this.blockID - 1];

        // Save current material for later reset
        restoreableMaterial = gameObject.GetComponent<Renderer>().material;
    }

    protected override void DestroyBlock()
    {
        base.DestroyBlock();
    }

    public override void ResetBlock()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Renderer>().material = restoreableMaterial;

        base.ResetBlock();
    }


    #endregion

    #region PlayerInteraction

    protected override void OnTouch(GameObject player)
    {
        // Destroy the lockBlock
        base.OnTouch(player);
    }

    protected override void OnTouchEnd(GameObject player)
    {
        base.OnTouchEnd(player);
    }

    #endregion

    public void RestoreBlock()
    {
        gameObject.GetComponent<Renderer>().material = restoredMaterial;
        gameObject.GetComponent<Collider>().enabled = true;
    }
}