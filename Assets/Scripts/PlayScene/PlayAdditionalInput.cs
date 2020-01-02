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
    private PlaySceneUI playUI;
    void Start()
    {
        playScene = this.GetComponent<PlayScene>();
        camControl = Camera.main.GetComponent<PlayCameraControl>();
        playUI = Gamemaster.Instance.GetPlaySceneUI();
        if (Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0] != "")
        {
            input = this.GetComponent<XboxInput>();
        }
        else
        {
            input = this.GetComponent<MouseAndKeyboardInput>();
        }
    }

    void Update()
    {
        if (!playUI.IsOpen)
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
        if (playUI.IsOpen && Gamemaster.Instance.GetLevelType() == LevelType.Main && input.PressedContinueButton())
        {
            playUI.NextLevel();
        }

    }
}
