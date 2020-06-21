using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Creates grid with levelSelectionButtons and initializes them
/// </summary>
[RequireComponent(typeof(GridLayoutGroup), typeof(RectTransform))]
public abstract class LevelButtonGroup : MonoBehaviour
{
    [SerializeField] protected GameObject buttonPrefab;
    [SerializeField] protected Scrollbar scrollbar;
    protected int levelCount;
    protected const float minHeight = 500f;
    protected const int LevelsPerRow = 5;
    protected int rows;
    protected abstract void Awake();
    /// <summary>
    /// Spawn buttons and initializes them
    /// </summary>
    /// <param name="fileNames"></param>
    protected abstract void SpawnButtons(string[] fileNames);

    #region Helper
    protected string ConvertFilePathToName(string filePath)
    {
        string[] parts = filePath.Split('/');
        return parts[parts.Length - 1].Replace(".dat", "");
    }

    protected void SetHeight()
    {
        GridLayoutGroup grid = this.GetComponent<GridLayoutGroup>();
        rows = ((levelCount - 1) / LevelsPerRow) + 1;
        float height = grid.padding.top + (grid.spacing.y + grid.cellSize.y) * rows;
        height = Mathf.Max(minHeight, height);
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(0, height);
    }

    protected void TestDirectory(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }

    protected GameObject InstantiateButtonAsChild()
    {
        GameObject buttonObject = Instantiate(buttonPrefab);
        buttonObject.transform.SetParent(this.transform);
        buttonObject.transform.localScale = Vector3.one;
        return buttonObject;
    }

    public void SelectButton(int id)
    {
        if (rows <= 1)
            return;
        scrollbar.value = 1f - 1f * ((id - 1) / LevelsPerRow) / (rows - 1);
    }
    #endregion
}
