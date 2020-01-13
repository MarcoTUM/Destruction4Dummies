using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class ProgressionFile
{
    private const string fileName = "progress.txt";
    private string filePath;
    
    public ProgressionFile()
    {
        Debug.Log("Pro");
        string folder = FilePaths.ProgressionFolder;
        if (!Directory.Exists(folder)) 
            Directory.CreateDirectory(folder);
        filePath = folder + fileName;
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "0");
        }
    }

    public int GetProgress()
    {
        string fileText = File.ReadAllText(filePath);
        string[] numberString = Regex.Split(fileText, @"\D+");
        if (numberString.Length != 1)
            throw new InvalidDataException("File " + fileName + " does not contain a single number");
        int number;
        if (int.TryParse(numberString[0], out number))
            return number;
        else
            throw new InvalidDataException("NumberString could not be parsed to Integer! " + numberString[0]);
    }

    public void SetProgress(int progress)
    {
        File.WriteAllText(filePath, progress + "");
    }
}
