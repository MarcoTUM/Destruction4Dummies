﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup), typeof(RectTransform))]
public class MainLevelSelection : LevelButtonGroup
{
    private string[] fileNames;
    protected override void Awake()
    {
        if (buttonPrefab.GetComponent<MainLevelButton>() == null)
            throw new InvalidOperationException($"ButtonPrefab does not have {nameof(MainLevelButton)}");
        TestDirectory(FilePaths.MainLevelFolder);
        fileNames = Directory.GetFiles(FilePaths.MainLevelFolder).
            Where(filePath => filePath.EndsWith(".dat")). //ignore meta files
            Select(filePath => ConvertFilePathToName(filePath)). //cut filePath to fileName
            OrderBy(fileName => GetLevelId(fileName)).ToArray<string>(); // sort fileNames by id 
        
    }

    private void Start()
    {
        SpawnButtons(fileNames);
        SetHeight();
    }

    protected override void SpawnButtons(string[] fileNames)
    {
        levelCount = Mathf.Min(fileNames.Length, Gamemaster.Instance.GetProgress()+1);
        for (int i = 0; i < levelCount; i++)
        {
            GameObject buttonObject = InstantiateButtonAsChild();
            buttonObject.GetComponent<MainLevelButton>().SetLevelId(GetLevelId(fileNames[i]));
            buttonObject.GetComponent<MainLevelButton>().SetSelection(this);
        }
    }

    private int GetLevelId(string fileName)
    {
        string id = Regex.Match(fileName, @"\d+").Value;
        return int.Parse(id);
    }

}
