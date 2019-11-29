using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float runningVelocity;
    public float fallSpeed;
    private readonly float fixedRotation = 0; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(fixedRotation, fixedRotation, fixedRotation);
        Rigidbody rigidbody = this.GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(0,-fallSpeed,0);
    }

    public void Run(float direction)
    {
        transform.Translate(new Vector3(direction * Time.deltaTime * runningVelocity, 0, 0));
    }

    //things needed: falling, colliding with walls, jumping

    public void JumpAction()
    {

    }
}
