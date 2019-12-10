using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{ 
    Start   = 0, 
    Goal    = 1, 
    Empty   = 2, 
    Wood    = 3, 
    Stone   = 4, 
    Chain   = 5 
};

public abstract class Block : MonoBehaviour
{
    public abstract Block_Data BlockData { get; set; }
    private const float MinLifeLoss = 0.3f;
    [SerializeField] private float lifeTime;
    protected float currentLifeTime;
    protected bool isTouchingPlayer = false;
    [SerializeField] private GameObject destructionAnimation;

    [SerializeField]
    protected Color blockColorGUI;

    #region Initialization / Destruction
    /// <summary>
    /// Sets the BlockData of a block and initializes the block (e.g. id for group blocks / timers for charge)
    /// </summary>
    /// <param name="data"></param>
    public virtual void InitializeBlock(Block_Data data)
    {
        BlockData = data;
    }

    protected virtual void DestroyBlock()
    {
        //toDo add fancy destruction animations per Block
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets the block to its original state
    /// </summary>
    public virtual void ResetBlock()
    {
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
        StartCoroutine(StartBlockDestruction());
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
        while (lifeTime > 0)
        {
            yield return new WaitForEndOfFrame();
            lifeTime -= Time.deltaTime;
        }
        DestroyBlock();
        Instantiate(destructionAnimation, transform.position, Quaternion.identity);
    }
    #endregion

    #region Helper

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
