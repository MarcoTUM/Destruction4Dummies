using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowawayBlock : MonoBehaviour
{
    public float secondsToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        if(secondsToDestroy < 0)
        {
            secondsToDestroy = 1;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject, secondsToDestroy);
        }
    }
}
