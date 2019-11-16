using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowawayBlock : MonoBehaviour
{
    public float secondsToDestroy = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject, secondsToDestroy);

            if(gameObject.tag.Equals("ChainBlock"))
            {
                GameObject[] chainBlocks = GameObject.FindGameObjectsWithTag("ChainBlock");
                foreach(GameObject chainBlock in chainBlocks)
                {
                    Destroy(chainBlock, secondsToDestroy);
                }
            }
        }
    }
}
