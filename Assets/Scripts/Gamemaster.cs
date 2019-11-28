using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemaster : Singleton<Gamemaster>
{
    public bool ApplicationQuit { get => IsApplicationQuitting; }
    private Level level;
    private LevelEditor editor;

    #region Level
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
}
