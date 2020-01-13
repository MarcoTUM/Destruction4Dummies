using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBlock : Block
{
    private Block_Data deathBlockData = new DeathBlock_Data();
    public override Block_Data BlockData { get => deathBlockData; set => deathBlockData = value; }
    [SerializeField] private float swapTimer;

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
        if (TouchedOnGoal())
            return;
        
        GameObject.FindObjectOfType<PlayScene>().KillPlayer();
        StartCoroutine(TextureSwap());
    }

    protected override void OnTouchEnd(GameObject player)
    {
        base.OnTouchEnd(player);
    }

    #endregion

    #region helper

    private IEnumerator TextureSwap()
    {
        Texture restoreTexture = gameObject.GetComponent<Renderer>().material.mainTexture;
        gameObject.GetComponent<Renderer>().material.mainTexture = destructionTexture;
        yield return new WaitForSeconds(swapTimer);
        gameObject.GetComponent<Renderer>().material.mainTexture = restoreTexture;
    }

    #endregion
}
