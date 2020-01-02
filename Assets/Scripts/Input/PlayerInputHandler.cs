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
    public bool IsUsingXbox;
    public bool IsInDialogue = false;

    private void Awake()
    {
        if (Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0] != "")
        {
            Debug.Log("Xbox input");
            IsUsingXbox = true;
            input = this.GetComponent<XboxInput>();
        }
        else
        {
            Debug.Log("Keybard input");
            IsUsingXbox = false;
            input = this.GetComponent<MouseAndKeyboardInput>();
        }

        player = this.GetComponent<Player>();
    }

    private void Update()
    {
        if (!player.IsOnGoal && !IsInDialogue)
            HandlePlayerInput();
        else if (IsInDialogue)
            HandleDialogueInput();
    }

    private void HandlePlayerInput()
    {
        player.Run(input.GetHorizontalDirection());

        if (input.PressedJump())
        {
            if (player.IsInteractingWithAdvisor)
                player.Interact();
            else
                player.JumpAction();
        }

        if (input.ReleasedJump())
            player.SetJumpRising(false);
        if (input.PressedSprintButton())
            player.ToggleSprint();
    }

    private void HandleDialogueInput()
    {
        if (input.PressedJump())
        {
            player.Interact();
        }
    }
}