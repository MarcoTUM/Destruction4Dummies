﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{ 
    Start       = 0, 
    Goal        = 1, 
    Empty       = 2, 
    Wood        = 3, 
    Stone       = 4, 
    Chain       = 5,
    Death       = 6,
    Lock        = 7,
    Key         = 8,
    Respawn     = 9,
    Charge      = 10,
    Restoreable = 11,
    Restore     = 12,
    Updraft     = 13
};

public abstract class Block : MonoBehaviour
{
    public abstract Block_Data BlockData { get; set; }
    private const float MinLifeLoss = 0.3f;
    [SerializeField] private float lifeTime;
    protected float currentLifeTime;
    protected bool isTouchingPlayer = false;
    [SerializeField] public Texture destructionTexture;
    private Texture recoveryTexture;

    private Coroutine blockDestructionCoroutine;
    #region Initialization / Destruction
    /// <summary>
    /// Sets the BlockData of a block and initializes the block (e.g. id for group blocks / timers for charge)
    /// </summary>
    /// <param name="data"></param>
    public virtual void InitializeBlock(Block_Data data)
    {
        BlockData = data;
        recoveryTexture = gameObject.GetComponent<Renderer>().material.mainTexture;
    }

    protected virtual void DestroyBlock()
    {
        // Set block inactive
        SpawnDestructionEffect();
        this.gameObject.SetActive(false);
        blockDestructionCoroutine = null;
        // Recover texture
        gameObject.GetComponent<Renderer>().material.mainTexture = recoveryTexture;
    }

    public void StartBlockDestructionCoroutine()
    {
        // Start destruction coroutine
        if(blockDestructionCoroutine == null)
            blockDestructionCoroutine = StartCoroutine(StartBlockDestruction());
    }

    public void StartInstantBlockDestruction()
    {
        DestroyBlock();
    }

    /// <summary>
    /// Sets the block to its original state
    /// </summary>
    public virtual void ResetBlock()
    {
        if (blockDestructionCoroutine != null)
        {
            StopCoroutine(blockDestructionCoroutine);
            blockDestructionCoroutine = null;
            gameObject.GetComponent<Renderer>().material.mainTexture = recoveryTexture;
        }
        this.gameObject.SetActive(true);
        lifeTime = currentLifeTime;
    }
    #endregion

    #region PlayerInteraction
    /// <summary>
    /// Handles interaction when player begins touching a block
    /// </summary>
    protected virtual void OnTouch(GameObject player)
    {
        isTouchingPlayer = true;
        if (TouchedOnGoal())
            return;
        if(Gamemaster.Instance.GetPlayer().canDestroy && blockDestructionCoroutine == null)
            blockDestructionCoroutine = StartCoroutine(StartBlockDestruction());
    }

    /// <summary>
    /// Handles interacton when player stops touching a block
    /// </summary>
    protected virtual void OnTouchEnd(GameObject player)
    {
        isTouchingPlayer = false;
    }

    /// <summary>
    /// Coroutine which goes through the lifeTime of the block and at the end calls its destruction
    /// Can override it to add animations / other effects
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator StartBlockDestruction()
    {
        gameObject.GetComponent<Renderer>().material.mainTexture = destructionTexture;
        while (lifeTime > 0)
        {
            yield return new WaitForEndOfFrame();
            lifeTime -= Time.deltaTime;
        }
        DestroyBlock();
    }
    #endregion

    #region Helper

    protected bool TouchedOnGoal()
    {
        if (Gamemaster.Instance.GetPlayer().IsOnGoal)
        {
            DestroyBlock();
            return true;
        }
        return false;
    }

    protected virtual void SpawnDestructionEffect()
    {
        Instantiate(EffectManager.Instance.GetEffect(0), transform.position, Quaternion.identity);
    }

    #endregion

    #region Unity

    protected virtual void Start()
    {
        currentLifeTime = lifeTime;
    }

    protected void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == TagDictionary.Player)
        {
            OnTouch(collider.gameObject);
        }
    }

    protected void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == TagDictionary.Player)
        {
            OnTouchEnd(collider.gameObject);
        }
    }

    #endregion
}
