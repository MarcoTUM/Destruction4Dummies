using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class LevelSaveLoad
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
        Level_Data level = null;
        try
        {
            string filePath = directoryPath + levelName + ".dat";
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            level = (Level_Data)bf.Deserialize(file);
            file.Close();
        }
        catch (FileNotFoundException)
        {
            
        }
        return level;
    }
    
    public static void Rename(string oldName, string newName, string directoryPath)
    {
        if (oldName == newName)
            return;
        File.Move(directoryPath + oldName + ".dat", directoryPath + newName + ".dat");
    }

    public static void Delete(string name, string directoryPath)
    {
        File.Delete(directoryPath + name + ".dat");
    }
}
