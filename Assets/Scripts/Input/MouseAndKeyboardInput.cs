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
        return Input.GetButtonDown(InputDictionary.Jump) || (Input.GetButtonDown(InputDictionary.Vertical) && Input.GetAxis(InputDictionary.Vertical) > 0);
    }

    public override bool PressedRestartButton()
    {
        return Input.GetKeyDown(KeyCode.R);
    }
}

