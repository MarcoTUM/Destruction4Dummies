using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseAndKeyboardInput : InputMethod
{

    public override float GetHorizontalDirection()
    {
        return Input.GetAxis(InputDictionary.Horizontal);
    }

    public override bool PressedJump()
    {
        return Input.GetButtonDown(InputDictionary.Jump) || (Input.GetButtonDown(InputDictionary.Vertical) && Input.GetAxis(InputDictionary.Vertical) > 0);
    }
}

