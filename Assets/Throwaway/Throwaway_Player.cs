using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Throwaway_Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpVelocity = 5;
    new private Rigidbody rigidbody;
    private float distanceToGround;

    private bool canDestroy { set; get; }
    public bool CanJump { set; get; }

    private void Awake()
    {
        canDestroy = true;
        rigidbody = this.GetComponent<Rigidbody>();
        distanceToGround = this.GetComponent<Collider>().bounds.extents.y;
    }

    public void MoveTo(float horizontalDir)
    {
        rigidbody.velocity = new Vector3(horizontalDir * moveSpeed, rigidbody.velocity.y, 0);
    }

    public void Jump()
    {
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpVelocity, 0);
        CanJump = false;
    }
    
    public bool IsGrounded()
    {
        return rigidbody.velocity.y < jumpVelocity / 2f && //stops groundchecks if jump just started
            Physics.Raycast(this.transform.position, Vector3.down, distanceToGround + 0.01f);//added offset for irregularities in ground
    }
}
