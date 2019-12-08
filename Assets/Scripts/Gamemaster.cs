using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemaster : Singleton<Gamemaster>
{
    public bool ApplicationQuit { get => IsApplicationQuitting; }
    private Level level;
    private PlayCameraControl playCamera;
    private PlaySceneUI playSceneUI;
    private LevelEditor editor;
    private Player player;

    private LevelType nextLevelType = LevelType.Main;
    private string nextLevelName;
    private int nextLevelId = 1;

    public GameObject blockDestrctionFX;
    

    #region Level
    public LevelType GetLevelType()
    {
        return nextLevelType;
    }

    public void SetNextMainLevelToLoad()
    {
        nextLevelType = LevelType.Main;
        nextLevelId++;
    }

    public void SetNextMainLevelToLoad(int levelId)
    {
        nextLevelType = LevelType.Main;
        nextLevelId = levelId;
    }

    public void SetNextCustomLevelToLoad(string levelName)
    {
        nextLevelType = LevelType.Custom;
        nextLevelName = levelName;
    }

    public void CreatePlayLevel()
    {
        if(level == null)
        {
            throw new InvalidOperationException("No level registered yet");
        }
        if(nextLevelType == LevelType.Main)
            level.LoadLevelFromFile("Level"+nextLevelId, FilePaths.MainLevelFolder);
        else if(nextLevelType == LevelType.Custom)
            level.LoadLevelFromFile(nextLevelName, FilePaths.CustomLevelFolder);
    }

    public void Register(Level level)
    {
        this.level = level;
    }

    public Level GetLevel()
    {
        return this.level;
    }
    #endregion

    #region LevelEditor
    public void Register(LevelEditor editor)
    {
        this.editor = editor;
    }

    public LevelEditor GetLevelEditor()
    {
        return this.editor;
    }
    #endregion

    #region PlayCameraControl
    public void Register(PlayCameraControl camera)
    {
        playCamera = camera;
    }

    public PlayCameraControl GetCameraPlayControl()
    {
        return playCamera;
    }
    #endregion

    #region PlaySceneUI
    public void Register(PlaySceneUI playUI)
    {
        this.playSceneUI = playUI;
    }

    public PlaySceneUI GetPlaySceneUI()
    {
        return this.playSceneUI;
    }
	#endregion
	
    #region Player

    public void Register(Player player)
    {
        this.player = player;
    }

    public Player GetPlayer()
    {
        return this.player;
    }

    #endregion

    public GameObject getBlockDestructionFX()
    {
        return blockDestrctionFX;
    }
}
