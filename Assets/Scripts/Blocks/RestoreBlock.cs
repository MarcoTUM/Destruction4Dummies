using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreBlock : Block
{
    private Block_Data restoreBlockData = new RestoreBlock_Data();
    public override Block_Data BlockData { get => restoreBlockData; set => restoreBlockData = value; }

    [SerializeField]
    private uint blockID = 0;

    public AudioClip audioClip;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    #region Initialization / Destruction
    public override void InitializeBlock(Block_Data data)
    {
        base.InitializeBlock(data);
        this.blockID = ((RestoreBlock_Data)data).GetID();
        this.GetComponent<Renderer>().material.color = RestoreBlock_Data.RestoreBlockColors[this.blockID - 1];
    }

    protected override void DestroyBlock()
    {
        base.DestroyBlock();
    }

    public override void ResetBlock()
    {
        base.ResetBlock();
    }


    #endregion

    #region PlayerInteraction

    protected override void OnTouch(GameObject player)
    {
        // If the player is able to destroy blocks
        if (Gamemaster.Instance.GetPlayer().canDestroy)
        {
            // Helper variable: Have all restoreable blocks been restored?
            bool allRestoreableBlocksAreRestored = true;

            // Find all restoreable blocks int the scene
            GameObject[] blocks = GameObject.FindGameObjectsWithTag(TagDictionary.RestoreableBlock);

            // Foreach restoreable block in the scene
            foreach (GameObject block in blocks)
            {
                // Get the RestoreableBlock script
                RestoreableBlock blockScript = block.GetComponent<RestoreableBlock>();

                // Get the block data
                RestoreableBlock_Data restoreableBlockData = (RestoreableBlock_Data)blockScript.BlockData;

                // If the restoreable block and the restore block have the same ID
                if (restoreableBlockData.GetID() == ((RestoreBlock_Data)BlockData).GetID())
                {
                    // If the block didn't get restored
                    if (!blockScript.RestoreBlock())
                    {
                        allRestoreableBlocksAreRestored = false;
                        blockScript.StartInstantBlockDestruction();
                    }
                }
            }

            //if (allRestoreableBlocksAreRestored)
            //{
                audioSource.PlayOneShot(audioClip, Random.Range(0.5f, 1.5f));
                base.OnTouch(player);
            //}
        }
    }

    protected override void OnTouchEnd(GameObject player)
    {
        base.OnTouchEnd(player);
    }

    #endregion
}