using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvisorDialogueCanvas : MonoBehaviour
{
    [SerializeField] private RectTransform thinkingBubble;
    [SerializeField] private RectTransform speechBubble;
    [SerializeField] private Text dialogueText;

    private string fullDialogue, leftoverDialogue;

    private void Start()
    {
        DisableInteractionBubble();
        fullDialogue = AdvisorDialogues.GetDialogues(0);
    }

    public void EnableInteraction()
    {
        thinkingBubble.gameObject.SetActive(true);
    }

    public void DisableInteractionBubble()
    {
        thinkingBubble.gameObject.SetActive(false);
        speechBubble.gameObject.SetActive(false);
    }

    public void ShowDialogue()
    {
        thinkingBubble.gameObject.SetActive(false);
        speechBubble.gameObject.SetActive(true);
        leftoverDialogue = fullDialogue;
        UpdateDialogueText();
    }

    public void NextPage()
    {
        if (leftoverDialogue == "")
            DisableInteractionBubble();
        UpdateDialogueText();
    }

    private void UpdateDialogueText()
    {
        dialogueText.text = leftoverDialogue;
        Canvas.ForceUpdateCanvases();
        if (dialogueText.cachedTextGenerator.characterCount > leftoverDialogue.Length)
        {
            leftoverDialogue = "";
        }
        else
            leftoverDialogue = leftoverDialogue.Substring(dialogueText.cachedTextGenerator.characterCount);
    }
}
