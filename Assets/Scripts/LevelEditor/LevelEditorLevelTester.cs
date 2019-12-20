using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorLevelTester : MonoBehaviour
{
    private Level level;
    private Player player;
    private PlayCameraControl cameraControl;
    private void Start()
    {
        level = Gamemaster.Instance.GetLevel();
        player = Gamemaster.Instance.GetPlayer();
        cameraControl = Gamemaster.Instance.GetCameraPlayControl();
        cameraControl.CameraFollow = false;
        player.gameObject.SetActive(false);
        
    }

    public void StartLevelTest()
    {
        Vector2Int startCoord = level.GetLevelData().StartPlatformCoordinates;
        player.SetStartPlatform(new Vector3(startCoord.x, startCoord.y, 0));
        player.SpawnAtSpawnPlatform();
        player.gameObject.SetActive(true);
        level.ShowEmptyBlocks(false);
        cameraControl.CameraFollow = true;
    }

}
