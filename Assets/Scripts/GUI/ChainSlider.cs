﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChainSlider : MonoBehaviour
{
    public Text sliderText;

    public void UpdateTextWithSliderValue(float sliderValue)
    {
        sliderText.text = ((int)(sliderValue + 1)).ToString();
    }
}
