using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemaster : Singleton<Gamemaster>
{
    public bool ApplicationQuit { get => IsApplicationQuitting; }
    private Level level;
    private PlayCameraControl playCamera;

    private LevelEditor editor;

    private string nextLevelName = "testLevel";
    private string nextLevelFolder = FilePaths.TestLevelFolderName;

    #region Level

    public void SetNextLevelToLoad(string levelName, string folder)
    {
        nextLevelName = levelName;
        nextLevelFolder = folder;
    }

    public void CreatePlayLevel()
    {
        if(level == null)
        {
            throw new InvalidOperationException("No level registered yet");
        }
        level.LoadLevelFromFile(nextLevelName, nextLevelFolder);
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
}
