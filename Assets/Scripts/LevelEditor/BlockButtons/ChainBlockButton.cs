using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChainBlockButton : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Slider slider;
    private ChainBlock_Data[] chainBlockData;
    private uint chainID = 0;

    private void Start()
    {
        chainBlockData = new ChainBlock_Data[(uint)slider.maxValue + 1];
        for(int i=0; i<chainBlockData.Length; i++)
        {
            chainBlockData[i] = new ChainBlock_Data((uint)i+1);
        }
    }

    public void SetChainID(float value)
    {
        Debug.Log((uint)value);
        chainID = (uint)value;
        ClickOnButton();
    }


    public void ClickOnButton()
    {
        Gamemaster.Instance.GetLevelEditor().SetCurrentBlock(chainBlockData[chainID]);
    }
}
