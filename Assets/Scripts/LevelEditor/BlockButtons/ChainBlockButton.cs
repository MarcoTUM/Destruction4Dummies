using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class ChainBlockButton : BlockButton
{
    [SerializeField] Slider slider;
    private ChainBlock_Data[] chainBlockData;
    private uint chainID = 0;

    protected void Awake()
    {
        chainBlockData = new ChainBlock_Data[(uint)slider.maxValue + 1];
        for(int i=0; i<chainBlockData.Length; i++)
        {
            chainBlockData[i] = new ChainBlock_Data((uint)i+1);
        }
    }

    public void SetChainID(float value)
    {
        if ((uint)value == chainID)
            return;
        chainID = (uint)value;
        this.GetComponent<Image>().color = ChainBlock_Data.BlockColors[chainID];
        this.GetComponent<Toggle>().SetIsOnWithoutNotify(true);
        OnValueChanged(true);
    }


    public override void OnValueChanged(bool active)
    {
        if (!active)
        {
            return;
        }
        Gamemaster.Instance.GetLevelEditor().SetCurrentBlock(chainBlockData[chainID]);
    }
}
