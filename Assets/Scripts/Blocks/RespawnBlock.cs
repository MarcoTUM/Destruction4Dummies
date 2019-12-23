using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBlock : Block
{
    private Block_Data respawnBlockData = new RespawnBlock_Data();
    public override Block_Data BlockData { get => respawnBlockData; set => respawnBlockData = value; }

    [SerializeField]
    private float respawnTime;

    private float touchTime;

    #region Initialization / Destruction
    public override void InitializeBlock(Block_Data data)
    {
        ((RespawnBlock_Data)BlockData).SetRespawnTime(respawnTime);
        base.InitializeBlock(data);
    }

    protected override void DestroyBlock()
    {
        GameObject.FindObjectOfType<PlayScene>().RespawnRespawnBlocks(this, respawnTime);
        base.DestroyBlock();
    }

    public override void ResetBlock()
    {
        base.ResetBlock();
    }

    #endregion

    /*
    private IEnumerator Respawn()
    {
        Debug.Log("Respawn coroutine 1! respawnTime: " + respawnTime);
        yield return new WaitForSeconds(respawnTime);
        Debug.Log("Respawn coroutine 2!");
        ResetBlock();
    }
    */

    #region PlayerInteraction

    protected override void OnTouch(GameObject player)
    {
        touchTime = Time.time;
        base.OnTouch(player);
    }

    protected override void OnTouchEnd(GameObject player)
    {
        base.OnTouchEnd(player);
    }

    #endregion
}
