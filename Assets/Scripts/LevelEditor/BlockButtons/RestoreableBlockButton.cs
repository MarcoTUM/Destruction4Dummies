using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]

public class RestoreableBlockButton : BlockButton
{
    [SerializeField] Slider slider;
    private RestoreableBlock_Data[] lockBlockData;
    private uint lockID = 0;

    protected void Awake()
    {
        lockBlockData = new RestoreableBlock_Data[(uint)slider.maxValue + 1];
        for (int i = 0; i < lockBlockData.Length; i++)
        {
            lockBlockData[i] = new RestoreableBlock_Data((uint)i + 1);
        }
    }

    public void SetID(float value)
    {
        if ((uint)value == lockID)
            return;
        lockID = (uint)value;
        this.GetComponent<Image>().color = RestoreableBlock_Data.RestoreableBlockColors[lockID];
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