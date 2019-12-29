using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvisorDialogueCanvas : MonoBehaviour
{
    [SerializeField] private RectTransform thinkingBubble;
    [SerializeField] private RectTransform speechBubble;
    [SerializeField] private Text dialogueText;

    [SerializeField] private RectTransform xboxSymbol, spaceBarSymbol;
    private bool showXboxSymbol;
    private string fullDialogue, leftoverDialogue;
    private bool showTextSlowly = false;
    [SerializeField] private float cps = 40;//Characters per Second
    private void Start()
    {
        DisableInteractionBubble();
        fullDialogue = AdvisorDialogues.GetDialogues(Gamemaster.Instance.GetLevelIndex());
        showXboxSymbol = Gamemaster.Instance.GetPlayer().GetComponent<PlayerInputHandler>().IsUsingXbox;
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
        xboxSymbol.gameObject.SetActive(showXboxSymbol);
        spaceBarSymbol.gameObject.SetActive(!showXboxSymbol);
    }

    /// <summary>
    /// Player moves out of interaction range => bubbles dissapear
    /// </summary>
    public void DisableInteractionBubble()
    {
        thinkingBubble.gameObject.SetActive(false);
        speechBubble.gameObject.SetActive(false);
        showTextSlowly = false;
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
    /// Or shows full dialogue when not fully shown yet
    /// </summary>
    /// <returns>true if next page exists otherwise false</returns>
    public bool NextPage()
    {
        if (showTextSlowly)
        {
            showTextSlowly = false;
            return true;
        }
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
        int characterCount = dialogueText.cachedTextGenerator.characterCount;
        if (characterCount > leftoverDialogue.Length)
        {
            characterCount = leftoverDialogue.Length;
            leftoverDialogue = "";
        }
        else
        {
            leftoverDialogue = leftoverDialogue.Substring(characterCount);
        }
        string showText = dialogueText.text.Substring(0, characterCount).Trim(); ;
        showTextSlowly = true;
        StartCoroutine(ShowTextSlowly(showText));
    }

    /// <summary>
    /// Prints text slowly onto speechBubble
    /// </summary>
    /// <param name="showText"></param>
    /// <returns></returns>
    private IEnumerator ShowTextSlowly(string showText)
    {
        string editedShowText = showText + "</color>";
        string colorInsert = "<color=#00000000>";
        for (int i = 0; i < showText.Length; i++)
        {
            if (!showTextSlowly)
                break;
            dialogueText.text = editedShowText.Insert(i, colorInsert);
            yield return new WaitForSeconds(1/cps);
        }
        dialogueText.text = showText;
        showTextSlowly = false;
    }
}
