using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]

public class KeyBlockButton : BlockButton
{
    [SerializeField] Slider slider;
    private KeyBlock_Data[] keyBlockData;
    private uint keyID = 0;

    protected void Awake()
    {
        keyBlockData = new KeyBlock_Data[(uint)slider.maxValue + 1];
        for (int i = 0; i < keyBlockData.Length; i++)
        {
            keyBlockData[i] = new KeyBlock_Data((uint)i + 1);
        }
    }

    public void SetChainID(float value)
    {
        if ((uint)value == keyID)
            return;
        keyID = (uint)value;
        this.GetComponent<Image>().color = KeyBlock_Data.BlockColors[keyID];
        this.GetComponent<Toggle>().SetIsOnWithoutNotify(true);
        OnValueChanged(true);
    }


    public override void OnValueChanged(bool active)
    {
        if (!active)
        {
            return;
        }
        Gamemaster.Instance.GetLevelEditor().SetCurrentBlock(keyBlockData[keyID]);
    }
}