using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdraftBlock : Block
{
    private Block_Data updraftBlockData;
    public override Block_Data BlockData { get => updraftBlockData; set => updraftBlockData = value; }
    private const float updraftVelocity = 10f;

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
        base.OnTouch(player);
        Player playerScript = player.GetComponent<Player>();
        playerScript.Updraft(updraftVelocity);
        Instantiate(EffectManager.Instance.GetEffect(6), transform.position, Quaternion.identity);
    }

    protected override void OnTouchEnd(GameObject player)
    {
        base.OnTouchEnd(player);
    }

    #endregion

    #region Helper

    protected override void SpawnDestructionEffect()
    {
        Instantiate(EffectManager.Instance.GetEffect(3), transform.position, Quaternion.identity);
    }

    #endregion


}
