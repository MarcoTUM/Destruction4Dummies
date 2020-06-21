using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(GridLayoutGroup), typeof(RectTransform))]
public class CustomLevelSelection : LevelButtonGroup
{
    private List<CustomLevelButton> buttons = new List<CustomLevelButton>();
    [SerializeField] private GameObject deleteButtonPrefab;
    private GameObject deleteButton;
    protected override void Awake()
    {
        if (buttonPrefab.GetComponent<CustomLevelButton>() == null)
            throw new InvalidOperationException($"ButtonPrefab does not have {nameof(CustomLevelButton)}");
        TestDirectory(FilePaths.CustomPlayLevelFolder);
        string[] fileNames = Directory.GetFiles(FilePaths.CustomPlayLevelFolder).
            Where(filePath => filePath.EndsWith(".dat")). //ignore meta files
            OrderBy(filePath => new FileInfo(filePath).CreationTime). // sort fileNames by id 
            Select(filePath => ConvertFilePathToName(filePath)).ToArray<string>(); //cut filePath to fileName

        levelCount = fileNames.Length + 1;
        SetHeight();
        SpawnButtons(fileNames);

    }

    protected override void SpawnButtons(string[] fileNames)
    {
        for (int i = 0; i < levelCount - 1; i++)
        {
            GameObject buttonObject = InstantiateButtonAsChild();
            CustomLevelButton button = buttonObject.GetComponent<CustomLevelButton>();
            button.SetLevelNameAndId(fileNames[i], i + 1);
            button.SetLevelSelection(this);
            buttons.Add(button);
        }

        if (buttons.Count > 0)
        {
            deleteButton = Instantiate(deleteButtonPrefab);
            deleteButton.transform.SetParent(this.transform);
            deleteButton.transform.localScale = Vector3.one;
            deleteButton.GetComponent<Button>().onClick.AddListener(SwitchDeleteMode);
        }
    }

    public void SwitchDeleteMode()
    {
        foreach (CustomLevelButton button in buttons)
        {
            button.SwitchDeleteMode();
        }
    }

    /// <summary>
    /// Remove button from list
    /// </summary>
    /// <param name="button"></param>
    public void RemoveLevel(CustomLevelButton button)
    {
        buttons.Remove(button);
        if (buttons.Count == 0)
            deleteButton.SetActive(false);
        else
            EventSystem.current.SetSelectedGameObject(deleteButton);
    }

}
