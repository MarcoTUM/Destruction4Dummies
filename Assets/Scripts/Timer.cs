using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text text;
    private void Start()
    {
        this.gameObject.SetActive(Gamemaster.Instance.ShowTimer());
        text = this.GetComponentInChildren<Text>();
    }

    void Update()
    {
        Gamemaster.Instance.UpdateTime();
        float time = Gamemaster.Instance.GetTime();
        int s = (int)time % 60;
        int min = ((int)time / 60) % 60;
        int h = ((int)time / 60 / 60);
        if(h == 0)
            text.text = System.String.Format("{0:00}:{1:00}", min, s);
        else
            text.text = System.String.Format("{0:00}:{1:00}:{2:00}", h, min, s);
    }
}
