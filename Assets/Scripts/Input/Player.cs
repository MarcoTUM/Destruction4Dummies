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
    private bool cielingCollision = false;
    public float dropFromCielingVelocity;
    
    private float xVelocity = 0f;

    //ray tracing
    public float horizRayMargin; //Margin: additional length to the ray (beyond the perpendicular surface of the player)
    public float horizRayPadding; //Padding: The distance between the non central rays and the parallel surface of the player object
    public float vertiRayMargin;
    public float vertiRayPadding;
    private float width;
    private float height;

    [SerializeField] private float respawnDuration = 1f;
    private Vector3 spawnPosition;
    private new Renderer[] renderers;
    #endregion

    #region Start, Update

    private void Start()
    {
    }

    private void Awake()
    {
        renderers = this.GetComponentsInChildren<Renderer>();
        Gamemaster.Instance.Register(this);
        height = transform.localScale.y;
        width = transform.localScale.x;
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
        Move(xVelocity,yVelocity);
        if (grounded)
            grounded = CheckGrounded();
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
    /// <summary>
    /// Converts input axis from the input handler to player velocity.x
    /// </summary>
    /// <param name="direction"> The direction of the input axis, raw. -1 for left, 1 for right</param>
    public void Run(float direction)
    {
        xVelocity = direction * runningVelocity;
    }
    /// <summary>
    /// Jump
    /// </summary>
    public void JumpAction()
    {
        if (grounded)
        {
            yVelocity += jumpVelocity;
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
        yVelocity = 0;
    }
    #endregion

    #region Ray cast collider detecion
    /// <summary>
    /// Checks if the player is still standing on ground, to be used only when the player is grounded (rest is handeld by ResolveYDistance)
    /// </summary>
    /// <returns></returns>
    private bool CheckGrounded()
    {
        return (
            Physics.Raycast(transform.position, new Vector3(0, -1, 0), height / 2 + vertiRayMargin) ||
            Physics.Raycast(transform.position - new Vector3(width / 2f + vertiRayPadding, 0, 0), new Vector3(0, -1, 0), height / 2 + vertiRayMargin) ||
            Physics.Raycast(transform.position + new Vector3(width / 2f - vertiRayPadding, 0, 0), new Vector3(0, -1, 0), height / 2 + vertiRayMargin)
            );
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
        return yVelocity * Time.deltaTime;
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
            if (Physics.Raycast(transform.position + i * new Vector3(0, height / 2f - horizRayPadding,0) , new Vector3(direction, 0, 0), out hit, width / 2f + horizRayMargin))
                return (hit.distance -width/2) * direction;
        }
        return xVelocity * Time.deltaTime;
    }
    #endregion

    #region Spawn and Death
    public void SetSpawnPosition(Vector3 spawnPosition)
    {
        this.spawnPosition = spawnPosition + Vector3.up * height/2f;
    }
    
    public void SpawnAtStartPlatform()
    {
        this.transform.position = spawnPosition;
        StartCoroutine(PlayerFadeIn());
    }
    private IEnumerator PlayerFadeIn() //test when playerModel available
    {
        float timer = 0;
        Color playerColor = Color.grey;
        while (timer < respawnDuration)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            playerColor.a = timer / respawnDuration;
            SetColor(playerColor);
        }
        SetColor(Color.white);
    }

    private void SetColor(Color color)
    {
        foreach(Renderer renderer in renderers)
        {
            renderer.material.color = color;
        }
    }
    #endregion
}
