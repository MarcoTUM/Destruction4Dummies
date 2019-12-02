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

    public float yAccel;
    private float yVelo = 0f;
    private bool grounded = false;
    private bool jumpingGrace = false;
    public float jumpingGraceTime;
    private bool isLanding = false;
    private bool cielingCollision = false;
    public float dropFromCielingVelocity;
    
    private float xVelo = 0f;

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

    //Update is called once per frame
    void Update()
    {
        Move(xVelo,yVelo);
    }

    #endregion

    #region Physics calculation and movement resolution

    /// <summary>
    /// Interprets the movement of the player,
    /// uses raycast to resolve movement behaviour
    /// </summary>
    /// <param name="xVelo">x axis velocity</param>
    /// <param name="yVelo">y axis velocity</param>
    private void Move(float xVelo, float yVelo)
    {
        //if airborne
        if (!grounded)
        {
            //and rising
            if (yVelo > 0)
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
        transform.Translate(xVelo * Time.deltaTime, yVelo * Time.deltaTime, 0);

        //post movement approximation here
        if (isLanding)
        {
            ApproximateYPosition();
            isLanding = false;
        }
            
    }

    private void FixedUpdate()
    {
        if (!grounded)
        {
            if (cielingCollision)
            {
                yVelo = -dropFromCielingVelocity;
                cielingCollision = false;
                return;
            }
            yVelo = yVelo + yAccel * Time.deltaTime;
            yVelo = Mathf.Clamp(yVelo, -fallVelocityLimit, jumpVelocity);
        }
    }
    #endregion

    #region Run and jump

    public void Run(float direction)
    {
        if (CastHoriRays(direction))
        {
            xVelo = 0;
            ApproximateXPosition(direction);
        }
        else
            xVelo = direction * runningVelocity;

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
    #endregion

    #region Auxiliry and approximation methods

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
