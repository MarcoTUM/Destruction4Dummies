using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Determines if button for specific method was pressed or not
/// </summary>
public abstract class InputMethod : MonoBehaviour
{
    public abstract float GetHorizontalDirection();
    public abstract bool PressedInteract();
    public abstract bool PressedJump();
    public abstract bool ReleasedJump();
    public abstract bool PressedExitButton();
    public abstract bool PressedRestartButton();
    public abstract bool PressedSprintButton();
    public abstract bool PressedZoomButton();
    public abstract bool ReleasedZoomButton();
    public abstract bool PressedContinueButton();
}
