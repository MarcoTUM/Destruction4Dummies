using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseAndKeyboardInput : InputMethod
{
    private enum PressedJumpButton { None, Space, W, Up };
    private PressedJumpButton pressedJumpButton = PressedJumpButton.None;
    public override float GetHorizontalDirection()
    {
        return Input.GetAxisRaw(InputDictionary.Horizontal);
    }

    public override bool PressedExitButton()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    public override bool PressedJump()
    {
        if (pressedJumpButton != PressedJumpButton.None)
            return false;
        if (Input.GetButtonDown(InputDictionary.Jump))
        {
            pressedJumpButton = PressedJumpButton.Space;
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            pressedJumpButton = PressedJumpButton.W;
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pressedJumpButton = PressedJumpButton.Up;
            return true;
        }
        return false;
    }

    public override bool PressedRestartButton()
    {
        return Input.GetKeyDown(KeyCode.R);
    }

    public override bool PressedSprintButton()
    {
        return Input.GetKeyDown(KeyCode.RightShift);
    }

    public override bool ReleasedJump()
    {
        if (pressedJumpButton == PressedJumpButton.None)
            return false;
        if (pressedJumpButton == PressedJumpButton.Space && Input.GetButtonUp(InputDictionary.Jump))
        {
            pressedJumpButton = PressedJumpButton.None;
            return true;
        }
        else if (pressedJumpButton == PressedJumpButton.W && Input.GetKeyUp(KeyCode.W))
        {
            pressedJumpButton = PressedJumpButton.None;
            return true;
        }
        else if (pressedJumpButton == PressedJumpButton.Up && Input.GetKeyUp(KeyCode.UpArrow))
        {
            pressedJumpButton = PressedJumpButton.None;
            return true;
        }
        return false;
    }

    public override bool PressedZoomButton()
    {
        return Input.GetKeyDown(InputDictionary.Zoom);
    }

    public override bool ReleasedZoomButton()
    {
        return Input.GetKeyUp(InputDictionary.Zoom);
    }

    public override bool PressedContinueButton()
    {
        return Input.GetKeyDown(KeyCode.Return);
    }
}

