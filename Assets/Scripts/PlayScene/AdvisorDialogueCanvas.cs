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
        fullDialogue = AdvisorDialogues.GetDialogues(Gamemaster.Instance.GetLevelIndex());
        if (fullDialogue == null)
            this.transform.parent.gameObject.SetActive(false);
    }

    /// <summary>
    /// Signalizes the player he can interact with Advisor
    /// </summary>
    public void EnableToughtBubble()
    {
        speechBubble.gameObject.SetActive(false);
        thinkingBubble.gameObject.SetActive(true);
    }

    /// <summary>
    /// Player moves out of interaction range => bubbles dissapear
    /// </summary>
    public void DisableInteractionBubble()
    {
        thinkingBubble.gameObject.SetActive(false);
        speechBubble.gameObject.SetActive(false);
    }

    /// <summary>
    /// Opens Speechbuble and shows first page of text
    /// </summary>
    public void ShowDialogue()
    {
        thinkingBubble.gameObject.SetActive(false);
        speechBubble.gameObject.SetActive(true);
        leftoverDialogue = fullDialogue;
        UpdateDialogueText();
    }

    /// <summary>
    /// tries to open next page of text
    /// </summary>
    /// <returns>true if next page exists otherwise false</returns>
    public bool NextPage()
    {
        if (leftoverDialogue == "")
        {
            DisableInteractionBubble();
            return false;
        }
        UpdateDialogueText();
        return true;
    }

    /// <summary>
    /// Updates current textbox and leftover text
    /// </summary>
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
