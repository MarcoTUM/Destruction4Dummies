using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LevelSaveLoad
{
    public static void Save(Level_Data level, string directoryPath)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string filePath = directoryPath + level.Name + ".dat";
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

    public static Level_Data Load(string levelName, string directoryPath)
    {
        string filePath = directoryPath + levelName + ".dat";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(filePath, FileMode.Open);
        Level_Data level = (Level_Data)bf.Deserialize(file);
        file.Close();
        return level;
    }



}
