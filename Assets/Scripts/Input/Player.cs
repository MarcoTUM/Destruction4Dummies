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
    public float sprintVelocity;
    public float fallVelocityLimit;
    public float jumpVelocity;

    public float fallAccelaration;
    public float riseAcceleration;
    private float yVelocity = 0f;
    private bool grounded = false;
    private bool cielingCollision = false;
    public float dropFromCielingVelocity;

    private float xVelocity = 0f;
    private bool isSprinting = true;

    //ray tracing
    public float horizRayMargin; //Margin: additional length to the ray (beyond the perpendicular surface of the player)
    public float horizRayPadding; //Padding: The distance between the non central rays and the parallel surface of the player object
    public float vertiRayMargin;
    public float vertiRayPadding;
    private float width;
    private float height;
    #endregion

    //animation
    private Animator animator;
    public float jumpToFallAnimationTime;
    private float lookRight = 100;
    private float lookLeft = 270;
    private bool isLookingRight = true;

    [HideInInspector] public bool IsOnGoal = false;
    private Renderer[] renderers;
    private Vector3 spawnPosition, goalPosition;
    
    
    #region Start, Update

    private void Awake()
    {
        renderers = this.GetComponentsInChildren<Renderer>();
        height = transform.localScale.y;
        width = transform.localScale.x;
        animator = GetComponentInChildren<Animator>();
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
        float xDistance = ResolveXDistance(xVelocity);
        float yDistance = ResolveYDistance(yVelocity);

        transform.Translate(xDistance, yDistance, 0);
    }

    private void FixedUpdate()
    {
        Move(xVelocity, yVelocity);
        if (grounded)
            grounded = CheckGrounded();
        if (!grounded)
        {
            if (cielingCollision)
            {
                yVelocity = -dropFromCielingVelocity;
                cielingCollision = false;
                StopCoroutine("AnimateJump");
                AnimateJumpToFall();
                return;
            }
            //case: rising
            if(yVelocity > 0 && Input.GetButton("Jump"))
                yVelocity = yVelocity + riseAcceleration * Time.fixedDeltaTime;
            //case: falling
            else 
                yVelocity = yVelocity + fallAccelaration * Time.fixedDeltaTime;
            //clamp
            yVelocity = Mathf.Clamp(yVelocity, -fallVelocityLimit, jumpVelocity);
        }
    }
    #endregion

    #region Run and jump
    /// <summary>
    /// Converts input axis from the input handler to player velocity.x
    /// </summary>
    /// <param name="direction"> The direction of the input axis, raw. -1 for left, 1 for right</param>
    public void Run(float direction)
    {
        if(isSprinting)
            xVelocity = direction * sprintVelocity;
        else
            xVelocity = direction * runningVelocity;
        if (direction == 0)
            animator.SetBool("isRunning", false);
        else if (grounded)
            animator.SetBool("isRunning", true);

        if (direction > 0 && !isLookingRight)
            SetModelRightDirection(true);
        else if (direction < 0 && isLookingRight)
            SetModelRightDirection(false);

    }
    /// <summary>
    /// Jump
    /// </summary>
    public void JumpAction()
    {
        if (grounded)
        {
            yVelocity += jumpVelocity;
            StartCoroutine("AnimateJump");
            grounded = false;
        }

    }
    #endregion

    #region Auxiliry
    /// <summary>
    /// Performs landing actions
    /// </summary>
    private void Land()
    {
        grounded = true;
        animator.SetBool("isFalling", false);
        yVelocity = 0;
    }

    /// <summary>
    /// Controls sprint modifier, called by input handler
    /// </summary>
    public void ToggleSprint()
    {
        isSprinting = !isSprinting;
    }
    
    #endregion

    #region Ray cast collider detecion
    /// <summary>
    /// Checks if the player is still standing on ground, to be used only when the player is grounded (rest is handeld by ResolveYDistance)
    /// </summary>
    /// <returns></returns>
    private bool CheckGrounded()
    {
        bool result = (
            Physics.Raycast(transform.position, new Vector3(0, -1, 0), height / 2 + vertiRayMargin) ||
            Physics.Raycast(transform.position - new Vector3(width / 2f + vertiRayPadding, 0, 0), new Vector3(0, -1, 0), height / 2 + vertiRayMargin) ||
            Physics.Raycast(transform.position + new Vector3(width / 2f - vertiRayPadding, 0, 0), new Vector3(0, -1, 0), height / 2 + vertiRayMargin)
            );
        if (!result)
            animator.SetBool("isFalling", true);
        return result;
    }

    /// <summary>
    /// Movement resolution using ray cast collision check for vertical movement
    /// </summary>
    /// <param name="yVelocity">The current player velocity.y</param>
    /// <returns>Returns the distance player travels with respect to the collisions</returns>
    private float ResolveYDistance(float yVelocity)
    {
        if (yVelocity == 0)
            return 0;
        float direction = yVelocity > 0 ? 1f : -1f;
        RaycastHit hit;
        for (int i = -1; i <= 1; i++)
        {
            if (Physics.Raycast(transform.position + i * new Vector3(width / 2f - vertiRayPadding, 0, 0), new Vector3(0, direction, 0), out hit, height / 2f + vertiRayMargin))
            {
                if (yVelocity > 0)
                    cielingCollision = true;
                else
                {
                    Land();
                    return (hit.distance - height / 2) * direction;
                }
            }
        }
        return yVelocity * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Movement resolution using ray cast collision check for horizontal movement
    /// </summary>
    /// <param name="xVelocity">The current player velocity.x</param>
    /// <returns>Returns the distance player travels with respect to the collisions</returns>
    private float ResolveXDistance(float xVelocity)
    {
        if (xVelocity == 0)
            return 0;
        float direction = xVelocity > 0 ? 1f : -1f;
        RaycastHit hit;
        for (int i = -1; i <= 1; i++)
        {
            if (Physics.Raycast(transform.position + i * new Vector3(0, height / 2f - horizRayPadding, 0), new Vector3(direction, 0, 0), out hit, width / 2f + horizRayMargin))
                return (hit.distance - width / 2) * direction;
        }
        return xVelocity * Time.fixedDeltaTime;
    }
    #endregion

    #region Animation

    private IEnumerator AnimateJump()
    {
        animator.SetBool("isJumping", true);
        yield return new WaitForSeconds(jumpToFallAnimationTime);
        AnimateJumpToFall();
    }

    private void AnimateJumpToFall()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isFalling", true);
        animator.SetBool("isRunning", false);
    }

    private void SetModelRightDirection(bool right)
    {
        if(right)
            transform.GetChild(0).transform.eulerAngles = new Vector3(0, lookRight, 0);
        else
            transform.GetChild(0).transform.eulerAngles = new Vector3(0, lookLeft, 0);
        isLookingRight = right;
    }

    #endregion

    #region Start End Level
    
    public void SetStartPlatform(Vector3 startPlatformPosition)
    {
        spawnPosition = startPlatformPosition + Vector3.up * (Block_Data.BlockSize + height) / 2f;
    }

    public void SpawnAtSpawnPlatform()
    {
        this.transform.position = spawnPosition;
    }

    /// <summary>
    /// Event called by GoalBlock that player reached the goal
    /// </summary>
    public void ReachGoalPlatform()
    {
        Vector2Int goalPlatformPosition = Gamemaster.Instance.GetLevel().GetLevelData().GoalPlatformCoordinates;
        this.GetComponent<Collider>().enabled = false;
        IsOnGoal = true;
        goalPosition = new Vector3(goalPlatformPosition.x, goalPlatformPosition.y + (Block_Data.BlockSize + height) / 2f, 0);
        StartCoroutine(GoalAnimation());
    }

    /// <summary>
    /// Animation where player walks to center of goalPlatform and jumps
    /// Jumping can be removed if it hinders level design
    /// </summary>
    /// <returns></returns>
    private IEnumerator GoalAnimation()
    {
        float xDistance = this.transform.position.x - goalPosition.x;
        int sign = (int)Mathf.Sign(-xDistance);
        while (IsOnGoal)
        {
            //prevent Player from falling to death after reaching goal
            if (this.transform.position.y - goalPosition.y < 0)
                this.transform.position = new Vector3(this.transform.position.x, goalPosition.y, this.transform.position.z);
            xDistance = this.transform.position.x - goalPosition.x;
            if (Mathf.Abs(xDistance) > Mathf.Epsilon && Mathf.Sign(-xDistance) == sign)
            {
                Run(sign/2f);
            }
            else
            {
                Run(0);
                JumpAction();
            }
            yield return new WaitForEndOfFrame();
        }
    }
    
    #endregion
}