using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]

public class RestoreBlockButton : BlockButton
{
    [SerializeField] Slider slider;
    private RestoreBlock_Data[] lockBlockData;
    private uint lockID = 0;

    protected void Awake()
    {
        lockBlockData = new RestoreBlock_Data[(uint)slider.maxValue + 1];
        for (int i = 0; i < lockBlockData.Length; i++)
        {
            lockBlockData[i] = new RestoreBlock_Data((uint)i + 1);
        }
    }

    public void SetChainID(float value)
    {
        if ((uint)value == lockID)
            return;
        lockID = (uint)value;
        this.GetComponent<Image>().color = RestoreBlock_Data.RestoreBlockColors[lockID];
        this.GetComponent<Toggle>().SetIsOnWithoutNotify(true);
        OnValueChanged(true);
    }


    public override void OnValueChanged(bool active)
    {
        if (!active)
        {
            return;
        }
        Gamemaster.Instance.GetLevelEditor().SetCurrentBlock(lockBlockData[lockID]);
    }
}