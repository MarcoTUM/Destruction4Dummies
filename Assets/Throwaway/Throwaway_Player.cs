using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class Throwaway_Player : MonoBehaviour
{
    [SerializeField] private float minY;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpVelocity = 5;
    new private Rigidbody rigidbody;
    private float distanceToGround;
    public float radius;
    public ParticleSystem forceOutbreak;

    public bool CanDestroy { set; get; }
    public bool CanJump { set; get; }

    private void Awake()
    {
        Physics.gravity *= 1.8f;
        CanDestroy = true;
        rigidbody = this.GetComponent<Rigidbody>();
        distanceToGround = this.GetComponent<Collider>().bounds.extents.y;
    }

    private void Update()
    {
        if(this.transform.position.y < minY)
        {
            Restart();
        }
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
        return //rigidbody.velocity.y < jumpVelocity / 2f && //stops groundchecks if jump just started
            Physics.Raycast(this.transform.position, Vector3.down, distanceToGround + 0.1f);//added offset for irregularities in ground
    }

    public void ForceOutbreak()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider hitCollider in hitColliders)
        {
            if (!hitCollider.gameObject.tag.Equals("Player"))
            {
                Instantiate(forceOutbreak, transform.position, Quaternion.identity);
                Destroy(hitCollider.gameObject);
            }
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
