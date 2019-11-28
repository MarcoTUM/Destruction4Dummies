using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemaster : Singleton<Gamemaster>
{
    public bool ApplicationQuit { get => IsApplicationQuitting; }
    private Level level;

    #region Level
    public void Register(Level level)
    {
        if(this.level != null)
        {
            throw new InvalidOperationException($"Trying to register second {nameof(level)}!");
        }
        else
        {
            this.level = level;
        }
    }

    public Level GetLevel()
    {
        return this.level;
    }
    #endregion

}
