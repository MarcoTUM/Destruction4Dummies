using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvisorDialogueCanvas : MonoBehaviour
{
    [SerializeField] private RectTransform thinkingBubble;

    private void Start()
    {
        thinkingBubble.gameObject.SetActive(false);
    }

    public void EnableInteraction()
    {
        thinkingBubble.gameObject.SetActive(true);
    }

    public void DisableInteraction()
    {

        thinkingBubble.gameObject.SetActive(false);
    }

}
