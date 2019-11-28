using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LevelSaveLoad
{
    public static void Save(Level_Data level, string subFolder = null)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string filePath = GetFilePath(level.Name, subFolder);
        string directoryPath = FilePaths.LevelFolder + subFolder;
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        FileStream file = File.Create(filePath);
        bf.Serialize(file, level);
        file.Close();
        Debug.Log("Saved at " + filePath);
    }

    public static Level_Data Load(string levelName, string subFolder = null)
    {
        string filePath = GetFilePath(levelName, subFolder);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(filePath, FileMode.Open);
        Level_Data level = (Level_Data)bf.Deserialize(file);
        file.Close();
        return level;
    }

    private static string GetFilePath(string levelName, string subFolder)
    {
        string filePath;
        if (subFolder == null)
        {
            filePath = FilePaths.LevelFolder + levelName + ".dat";
        }
        else
        {
            if (!subFolder.EndsWith("/"))
                subFolder += "/";
            filePath = FilePaths.LevelFolder + subFolder + levelName + ".dat";
        }
        return filePath;
    }

}
