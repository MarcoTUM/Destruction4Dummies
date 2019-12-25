using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseAndKeyboardInput : InputMethod
{

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
        return Input.GetButtonDown(InputDictionary.Jump);
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
        return Input.GetButtonUp(InputDictionary.Jump);
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

