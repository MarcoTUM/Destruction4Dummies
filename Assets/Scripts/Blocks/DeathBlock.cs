using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBlock : Block
{
    private Block_Data deathBlockData = new DeathBlock_Data();
    public override Block_Data BlockData { get => deathBlockData; set => deathBlockData = value; }

    #region Initialization / Destruction
    public override void InitializeBlock(Block_Data data)
    {
        base.InitializeBlock(data);
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
        // GameObject.FindGameObjectWithTag(TagDictionary.PlayScene).GetComponent<PlayScene>().KillPlayer();
        GameObject.FindObjectOfType<PlayScene>().KillPlayer();
    }

    protected override void OnTouchEnd(GameObject player)
    {
        base.OnTouchEnd(player);
    }

    #endregion
}
