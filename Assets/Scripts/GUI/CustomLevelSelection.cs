using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup), typeof(RectTransform))]
public class CustomLevelSelection : LevelButtonGroup
{
    protected override void Awake()
    {
        if (buttonPrefab.GetComponent<CustomLevelButton>() == null)
            throw new InvalidOperationException($"ButtonPrefab does not have {nameof(CustomLevelButton)}");
        TestDirectory(FilePaths.CustomPlayLevelFolder);
        string[] fileNames = Directory.GetFiles(FilePaths.CustomPlayLevelFolder).
            Where(filePath => filePath.EndsWith(".dat")). //ignore meta files
            OrderBy(filePath => new FileInfo(filePath).CreationTime). // sort fileNames by id 
            Select(filePath => ConvertFilePathToName(filePath)).ToArray<string>(); //cut filePath to fileName
        
        levelCount = fileNames.Length;
        SetHeight();
        SpawnButtons(fileNames);

    }

    protected override void SpawnButtons(string[] fileNames)
    {
        for (int i = 0; i < levelCount; i++)
        {
            GameObject buttonObject = InstantiateButtonAsChild();
            buttonObject.GetComponent<CustomLevelButton>().SetLevelName(fileNames[i]);
        }
    }

}
