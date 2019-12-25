using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MouseAndKeyboardInput), typeof(XboxInput), typeof(PlayScene))]
public class PlayAdditionalInput : MonoBehaviour
{
    private InputMethod input; //either Keyboard or XboxInput
    private PlayScene playScene;
    private PlayCameraControl camControl;

    void Start()
    {
        playScene = this.GetComponent<PlayScene>();
        camControl = Camera.main.GetComponent<PlayCameraControl>();
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
        if (input.PressedZoomButton())
        {
            camControl.StartZoomOut();
        }
        else if (input.ReleasedZoomButton())
        {
            camControl.StartZoomIn();
        }

        if (input.PressedExitButton())
        {
            SceneManager.LoadScene(Gamemaster.Instance.GetLevelType() == LevelType.Test ? SceneDictionary.LevelEditor : SceneDictionary.MainMenu);
        }
        else if (input.PressedRestartButton())
        {
            playScene.KillPlayer();
        }
        
    }
}
