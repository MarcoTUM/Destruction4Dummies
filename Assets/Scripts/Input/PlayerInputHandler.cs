using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all Input for player object and calls respective player methods when needed
/// </summary>
[RequireComponent(typeof(MouseAndKeyboardInput), typeof(XboxInput))]
public class PlayerInputHandler : MonoBehaviour
{
    private InputMethod input; //either Keyboard or XboxInput
    private Player player;

    private void Awake()
    {
        if (Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0] != "")
        {
            Debug.Log("Xbox input");
            input = this.GetComponent<XboxInput>();
        }
        else
        {
            Debug.Log("Keybard input");
            input = this.GetComponent<MouseAndKeyboardInput>();
        }

        player = this.GetComponent<Player>();
    }

    private void Update()
    {
        if (!player.IsOnGoal)
            HandlePlayerInput();
    }

    private void HandlePlayerInput()
    {
        player.Run(input.GetHorizontalDirection());
        if (input.PressedJump())
            player.JumpAction();
        if (input.ReleasedJump())
            player.SetJumpRising(false);
        if(input.PressedSprintButton())
            player.ToggleSprint();
    }
}
