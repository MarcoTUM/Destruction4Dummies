using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Throwaway_Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float jumpAcceleration = 10f;
    new private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }

    public void MoveTo(float horizontalDir)
    {
        rigidbody.velocity = new Vector3(horizontalDir * moveSpeed, rigidbody.velocity.y, 0);
        Debug.Log(rigidbody.velocity);
    }

    public void Jump()
    {
        //to do add check if touches ground
        rigidbody.AddForce(Vector3.up * jumpAcceleration, ForceMode.Impulse);
    }
}
