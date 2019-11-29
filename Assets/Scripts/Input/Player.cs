using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float runningVelocity;
    public float fallVelocityLimit;
    public float jumpVelocity;
    private readonly float fixedRotation = 0;

    private float yAccel = -10f;
    private float yVelo = 0f;
    private bool grounded = false;
    private bool jumpingGrace = false;
    public float jumpingGraceTime;
    
    private float xVelo = 0f;

    //ray tracing
    public float horizRayMargin;
    public float verticalRayLen;
    public float bottomRayPadding;
    private float width = 1f;
    private float height = 2f;

    //Update is called once per frame
    void Update()
    {
        Debug.Log(grounded);
        Move(xVelo,yVelo);
    }

    private void Move(float xVelo, float yVelo)
    {

        if (!grounded && !jumpingGrace && yVelo < 0)
        {
            if (
                Physics.Raycast(transform.position, new Vector3(0, -1, 0), verticalRayLen) ||
                Physics.Raycast(transform.position - new Vector3(width / 2f, 0, 0), new Vector3(0, -1, 0), verticalRayLen) ||
                Physics.Raycast(transform.position + new Vector3(width / 2f, 0, 0), new Vector3(0, -1, 0), verticalRayLen)
                )
            {
                Land();
            }
        }
        else if (
            !Physics.Raycast(transform.position, new Vector3(0, -1, 0), verticalRayLen) &&
            !Physics.Raycast(transform.position - new Vector3(width / 2f, 0, 0), new Vector3(0, -1, 0), verticalRayLen) &&
            !Physics.Raycast(transform.position + new Vector3(width / 2f, 0, 0), new Vector3(0, -1, 0), verticalRayLen)
            )
            grounded = false;
        
            //resolve
            transform.Translate(xVelo * Time.deltaTime, yVelo * Time.deltaTime, 0);

    }

    private void FixedUpdate()
    {
        if (!grounded)
        {
            yVelo = yVelo + yAccel * Time.deltaTime;
            yVelo = Mathf.Clamp(yVelo, -fallVelocityLimit, jumpVelocity);
        }
    }

    public void Run(float direction)
    {
        if (
            !Physics.Raycast(transform.position, new Vector3(direction, 0, 0), width/2f + horizRayMargin) &&
            !Physics.Raycast(transform.position - new Vector3(0, height / 2f + bottomRayPadding,0), new Vector3(direction, 0, 0), width / 2f + horizRayMargin) &&
            !Physics.Raycast(transform.position + new Vector3(0, height / 2f, 0), new Vector3(direction, 0, 0), width / 2f + horizRayMargin)
            )
            xVelo = direction * runningVelocity;
        else
            xVelo = 0;
    }

    public void JumpAction()
    {
        if (grounded)
        {
            yVelo += jumpVelocity;
            grounded = false;
            jumpingGrace = true;
            StartCoroutine("HandleJumpGrace");
        }
            
    }

    private void Land()
    {
        grounded = true;
        yVelo = 0;
    }

    IEnumerator HandleJumpGrace()
    {
        yield return new WaitForSeconds(jumpingGraceTime);
        jumpingGrace = false;
    }
}
