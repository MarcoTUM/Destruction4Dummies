using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script controls player character behaviour
/// </summary>
public class Player : MonoBehaviour
{
    #region Fields

    public float runningVelocity;
    public float fallVelocityLimit;
    public float jumpVelocity;

    public float yAcceleration;
    private float yVelocity = 0f;
    private bool grounded = false;
    private bool jumpingGrace = false;
    public float jumpingGraceTime;
    private bool isLanding = false;
    private bool cielingCollision = false;
    public float dropFromCielingVelocity;
    
    private float xVelocity = 0f;

    public float floatMargin;
    public float wallMargin;

    //ray tracing
    public float horizRayMargin;
    public float downRayMargin;
    public float upRayMargin;
    public float bottomRayMargin;
    public float topRayMargin;
    private float width;
    private float height;
    #endregion

    #region Start, Update

    private void Start()
    {
        height = transform.localScale.y;
        width = transform.localScale.x;
    }

    private void Awake()
    {
        Gamemaster.Instance.Register(this);
    }

    #endregion

    #region Physics calculation and movement resolution

    /// <summary>
    /// Interprets the movement of the player,
    /// uses raycast to resolve movement behaviour
    /// </summary>
    /// <param name="xVelocity">x axis velocity</param>
    /// <param name="yVelocity">y axis velocity</param>
    private void Move(float xVelocity, float yVelocity)
    {
        //if airborne
        if (!grounded)
        {
            //and rising
            if (yVelocity > 0)
            {
                //detect collision with above objects
                if (CastRaysUp())
                {
                    cielingCollision = true;
                }
            }
            //if falling, and jumpingGrace has passed
            else if(!jumpingGrace)
            {
                //detect collision with the ground
                if (CastRaysDown())
                {
                    Land();
                    isLanding = true;
                }
            }
        }

        //if on the ground, detect falling
        else if (!CastRaysDown())
        {
            grounded = false;
        }

        //resolve movement
        transform.Translate(xVelocity * Time.deltaTime, yVelocity * Time.deltaTime, 0);

        //post movement approximation here
        if (isLanding)
        {
            ApproximateYPosition();
            isLanding = false;
        }
            
    }

    private void FixedUpdate()
    {
        Move(xVelocity,yVelocity);
        if (!grounded)
        {
            if (cielingCollision)
            {
                yVelocity = -dropFromCielingVelocity;
                cielingCollision = false;
                return;
            }
            yVelocity = yVelocity + yAcceleration * Time.deltaTime;
            yVelocity = Mathf.Clamp(yVelocity, -fallVelocityLimit, jumpVelocity);
        }
    }
    #endregion

    #region Run and jump

    public void Run(float direction)
    {
        if (CastHoriRays(direction))
        {
            xVelocity = 0;
            ApproximateXPosition(direction);
        }
        else
            xVelocity = direction * runningVelocity;

    }

    public void JumpAction()
    {
        if (grounded)
        {
            yVelocity += jumpVelocity;
            grounded = false;
            jumpingGrace = true;
            StartCoroutine("HandleJumpGrace");
        }
            
    }
    #endregion

    #region Auxiliry and approximation methods

    private void Land()
    {
        grounded = true;
        yVelocity = 0;
    }

    IEnumerator HandleJumpGrace()
    {
        yield return new WaitForSeconds(jumpingGraceTime);
        jumpingGrace = false;
    }

    private void ApproximateYPosition()
    {
        float yValue = Mathf.Floor(transform.position.y) + 0.5f - (2 - height)/2;
        yValue += floatMargin;
        transform.position = new Vector3(transform.position.x, yValue, transform.position.z);
    }

    private void ApproximateXPosition(float direction)
    {
        float xValue = Mathf.Round(transform.position.x);
        xValue += -1f * direction * wallMargin;
        transform.position = new Vector3(xValue, transform.position.y, transform.position.z);
    }
    #endregion

    #region Ray cast collider detecion

    /// <summary>
    /// Ray cast collision check for down direction of player movement
    /// </summary>
    /// <returns></returns>
    private bool CastRaysDown()
    {
        return (Physics.Raycast(transform.position, new Vector3(0, -1, 0) , height / 2 + downRayMargin) ||
                Physics.Raycast(transform.position - new Vector3(width / 2f, 0, 0), new Vector3(0, -1, 0) , height / 2 + downRayMargin) ||
                Physics.Raycast(transform.position + new Vector3(width / 2f, 0, 0), new Vector3(0, -1, 0) , height / 2 + downRayMargin));
    }
    /// <summary>
    /// Ray cast collision check for the up direction of player movement
    /// </summary>
    /// <returns></returns>
    private bool CastRaysUp()
    {
        return (Physics.Raycast(transform.position, new Vector3(0, 1 , 0), height / 2 + upRayMargin) ||
                Physics.Raycast(transform.position - new Vector3(width / 2f, 0, 0), new Vector3(0, 1 , 0), height / 2 + upRayMargin) ||
                Physics.Raycast(transform.position + new Vector3(width / 2f, 0, 0), new Vector3(0, 1 , 0), height / 2 + upRayMargin));
    }
    /// <summary>
    /// Ray cast collision check for horizontal player movement
    /// </summary>
    /// <param name="direction">the raw direction as it is given from the input handler</param>
    /// <returns></returns>
    private bool CastHoriRays(float direction)
    {
        return (Physics.Raycast(transform.position, new Vector3(direction, 0, 0), width / 2f + horizRayMargin) ||
            Physics.Raycast(transform.position - new Vector3(0, height / 2f  - bottomRayMargin, 0), new Vector3(direction, 0, 0), width / 2f + horizRayMargin) ||
            Physics.Raycast(transform.position + new Vector3(0, height / 2f - topRayMargin, 0), new Vector3(direction, 0, 0), width / 2f + horizRayMargin));
    }
    #endregion

}
