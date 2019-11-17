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
    private Throwaway_Player player; //toDo replace with proper PlayerScript

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

        player = this.GetComponent<Throwaway_Player>();
    }

    private void Update()
    {
        HandlePlayerInput();
    }

    private void HandlePlayerInput()
    {
        player.MoveTo(input.GetHorizontalDirection());
        if (player.IsGrounded() && input.PressedJump())
        {
            player.Jump();
        }
        
    }
}
