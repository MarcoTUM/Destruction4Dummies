using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    private float timeToKill;

    // Start is called before the first frame update
    void Start()
    {
        //set timeToKill
        UnityEngine.ParticleSystem.MainModule module = gameObject.GetComponent<ParticleSystem>().main;
        timeToKill = module.duration + module.startLifetime.constant;
        StartCoroutine("KillParticleEffect");
    }

    private IEnumerator KillParticleEffect()
    {
        yield return new WaitForSeconds(timeToKill);
        Destroy(gameObject);
    }
}
