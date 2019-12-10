using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MouseAndKeyboardInput), typeof(XboxInput), typeof(PlayScene))]
public class PlayExitRestart : MonoBehaviour
{
    private InputMethod input; //either Keyboard or XboxInput
    private PlayScene playScene;
    void Start()
    {
        playScene = this.GetComponent<PlayScene>();
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
    }
    
    void Update()
    {
        if (input.PressedExitButton())
        {
            SceneManager.LoadScene(SceneDictionary.MainMenu);
        }
        else if (input.PressedRestartButton())
        {
            playScene.KillPlayer();
        }
    }
}
