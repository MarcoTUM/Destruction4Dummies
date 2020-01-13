using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBlock : Block
{
    private Block_Data chainBlockData = new ChainBlock_Data();
    public override Block_Data BlockData { get => chainBlockData; set => chainBlockData = value; }

    [SerializeField]
    private uint blockID = 0;

    public AudioClip audioClip;
    private AudioSource audioSource;

    #region Initialization / Destruction
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public override void InitializeBlock(Block_Data data)
    {
        base.InitializeBlock(data);
        this.blockID = ((ChainBlock_Data)data).GetChainID();
        this.GetComponent<Renderer>().material.color = ChainBlock_Data.ChainBlockColors[this.blockID - 1];
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
        //base.OnTouch(player);
        if (TouchedOnGoal())
            return;
        if (!Gamemaster.Instance.GetPlayer().canDestroy)
            return;

        GameObject[] chainBlocks = GameObject.FindGameObjectsWithTag("ChainBlock");
        foreach (GameObject chainBlock in chainBlocks)
        {
            ChainBlock chainBlockScript = chainBlock.GetComponent<ChainBlock>();

            if (chainBlockScript.blockID == ((ChainBlock_Data)BlockData).GetChainID())
            {
                chainBlockScript.StartCoroutine("StartBlockDestruction");
            }
        }

        audioSource.PlayOneShot(audioClip, Random.Range(0.5f, 1.5f));
    }

    protected override void OnTouchEnd(GameObject player)
    {
        base.OnTouchEnd(player);
    }

    #endregion

    #region helper

    protected override void SpawnDestructionEffect()
    {
        Instantiate(EffectManager.Instance.GetEffect(8), transform.position, Quaternion.identity);
    }

    #endregion
}
