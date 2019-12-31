using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FilePaths
{
    //Stream:
    public static string CustomEditLevelFolder = Application.persistentDataPath + "/CustomLevels/Editor/";
    public static string CustomPlayLevelFolder = Application.persistentDataPath + "/CustomLevels/Play/";
    public static string MainLevelFolder = Application.streamingAssetsPath + "/Levels/";

    //Resources:
    public static string TextResourceFolder = "Texts/";
}
