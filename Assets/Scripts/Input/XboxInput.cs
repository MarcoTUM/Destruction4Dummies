using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class XboxInput : InputMethod
{
    private bool zoomPressed = false;

    public override float GetHorizontalDirection()
    {
        float inputDir = Input.GetAxisRaw(InputDictionary.XboxLeftJoystickHorizontal);
        if (inputDir > 0)
            return 1f;
        else if (inputDir < 0)
            return -1f;
        else
            return 0f;
    }

    public override bool PressedSprintButton()
    {
        return Input.GetKeyDown(InputDictionary.XboxBButton);//Not sure which button
    }

    public override bool PressedExitButton()
    {
        return Input.GetKeyDown(InputDictionary.XboxStartButton);
    }

    public override bool PressedJump()
    {
        return Input.GetKeyDown(InputDictionary.XboxAButton);
    }

    public override bool PressedRestartButton()
    {
        return Input.GetKeyDown(InputDictionary.XboxBackButton);//Not sure if it would be better to switch restart and exit button;
    }

    public override bool ReleasedJump()
    {
        return Input.GetKeyUp(InputDictionary.XboxAButton);
    }

    public override bool PressedZoomButton()
    {
        if (zoomPressed)
            return false;
        if (Input.GetAxis(InputDictionary.XboxRightTrigger) > 0 || Input.GetAxis(InputDictionary.XboxLeftTrigger) > 0)
        {
            zoomPressed = true;
            return true;
        }
        return false;
    }

    public override bool ReleasedZoomButton()
    {
        if (zoomPressed && Input.GetAxis(InputDictionary.XboxRightTrigger) <= 0 && Input.GetAxis(InputDictionary.XboxLeftTrigger) <= 0)
        {
            zoomPressed = false;
            return true;
        }
        return false;
    }

    public override bool PressedContinueButton()
    {
        return Input.GetKeyDown(InputDictionary.XboxStartButton);
    }

    public override bool PressedInteract()
    {
        return Input.GetKeyDown(InputDictionary.XboxAButton);
    }
    
}
