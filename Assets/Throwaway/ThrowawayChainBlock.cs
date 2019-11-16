using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowawayChainBlock : ThrowawayBlock
{
    public short blockID;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player") && collision.gameObject.GetComponent<Throwaway_Player>().CanDestroy)
        {
            Destroy(gameObject, secondsToDestroy);

            GameObject[] chainBlocks = GameObject.FindGameObjectsWithTag("ChainBlock");
            foreach (GameObject chainBlock in chainBlocks)
            {
                if (chainBlock.GetComponent<ThrowawayChainBlock>().blockID == blockID)
                {
                    Destroy(chainBlock, secondsToDestroy);
                }
            }
        }
    }
}
