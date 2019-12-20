using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof(GridLayoutGroup), typeof(RectTransform), typeof(ToggleGroup))]
public class BlockSelection : MonoBehaviour
{
    [SerializeField] private Toggle startBlock;

    /// <summary>
    /// Initializes the Rect for the scroll view + selects initial Block 
    /// </summary>
    void Start()
    {
        int childCount = this.transform.childCount;
        GridLayoutGroup grid = this.GetComponent<GridLayoutGroup>();
        float padding = grid.padding.left + grid.padding.right;
        float spacePerBlock = grid.cellSize.x + grid.spacing.x;
        float width = Mathf.Max(padding + childCount * spacePerBlock, 1302); //I dont know why there are 2 empty pixels(unity is weird)
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 0);

        var toggles = this.GetComponent<ToggleGroup>().ActiveToggles();
        foreach (Toggle toggle in toggles)
        {
            if (toggle.isOn)
            {
                throw new System.InvalidOperationException("Toggle Group has active toggle: " + toggle.gameObject.name);
            }
        }
        startBlock.isOn = true;
        startBlock.Select();
        this.GetComponent<ToggleGroup>().allowSwitchOff = false;
    }
}
