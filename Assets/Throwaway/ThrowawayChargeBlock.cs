using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowawayChargeBlock : ThrowawayBlock
{
    public bool IsCharged { set; get; }
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        IsCharged = true;
    }

    private void Update()
    {
        if(!IsCharged && Time.time - startTime > 10.0f)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Throwaway_Player>().CanDestroy = true;
            Destroy(gameObject ,secondsToDestroy);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (IsCharged
                && collision.gameObject.tag.Equals("Player")
                && collision.gameObject.GetComponent<Throwaway_Player>().CanDestroy)
        {
            collision.gameObject.GetComponent<Throwaway_Player>().CanDestroy = false;
            IsCharged = false;
            startTime = Time.time;
        }
    }
}
