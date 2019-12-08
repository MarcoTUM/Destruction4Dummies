using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    public float timeToKill;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("KillParticleEffect");
    }

    private IEnumerator KillParticleEffect()
    {
        yield return new WaitForSeconds(timeToKill);
        Destroy(gameObject);
    }
}
