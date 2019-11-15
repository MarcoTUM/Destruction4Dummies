using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemaster : Singleton<Gamemaster>
{
    public bool ApplicationQuit { get => IsApplicationQuitting; }

}
