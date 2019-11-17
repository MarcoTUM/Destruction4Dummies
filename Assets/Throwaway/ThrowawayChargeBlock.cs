using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowawayChargeBlock : ThrowawayBlock
{
    public bool IsCharged { set; get; }
    private float startTime;
    public float chargeTimer;

    // Start is called before the first frame update
    void Start()
    {
        IsCharged = true;

        if (chargeTimer < 0)
            chargeTimer = 5.0f;
    }

    private void Update()
    {
        if(!IsCharged && Time.time - startTime > chargeTimer)
        {
            Throwaway_Player throwaway_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Throwaway_Player>();
            throwaway_Player.CanDestroy = true;
            Destroy(gameObject ,secondsToDestroy);
            throwaway_Player.ForceOutbreak();
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
