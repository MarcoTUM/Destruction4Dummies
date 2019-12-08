using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionTabs : MonoBehaviour
{
    [SerializeField] private GameObject mainLevelView, customLevelView;
    public void MainLevelTab(bool active)
    {
        mainLevelView.SetActive(active);
    }

    public void CustomLevelTab(bool active)
    {
        customLevelView.SetActive(active);
    }
}
