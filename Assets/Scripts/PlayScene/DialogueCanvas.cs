using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueCanvas : MonoBehaviour
{
    [SerializeField] private RectTransform speechBubble;
    [SerializeField] private Text dialogueText;
    private string fullText, leftoverText;
    private bool showTextSlowly = false;
    [SerializeField] private float cps = 40;//Characters per Second

    protected virtual void Start()
    {
        DisableOpenPanels();
    }

    /// <summary>
    /// Initializes the canvas with a new string to print and prints first page of this string
    /// </summary>
    /// <param name="line"></param>
    public void PrintLine(string line)
    {
        fullText = line;
        PrintText();
    }

    /// <summary>
    /// Closes all open Panels in the Canvas
    /// </summary>
    public virtual void DisableOpenPanels()
    {
        speechBubble.gameObject.SetActive(false);
        showTextSlowly = false;
    }

    /// <summary>
    /// Opens Speechbuble and shows first page of text
    /// </summary>
    public void PrintText()
    {
        this.transform.rotation = Quaternion.identity;
        speechBubble.gameObject.SetActive(true);
        leftoverText = fullText;
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
        if (leftoverText == "")
        {
            DisableOpenPanels();
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
        dialogueText.text = leftoverText;
        Canvas.ForceUpdateCanvases();
        int characterCount = dialogueText.cachedTextGenerator.characterCount;
        if (characterCount > leftoverText.Length)
        {
            characterCount = leftoverText.Length;
            leftoverText = "";
        }
        else
        {
            leftoverText = leftoverText.Substring(characterCount);
        }
        string showText = dialogueText.text.Substring(0, characterCount).Trim();
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
            yield return new WaitForSeconds(1 / cps);
        }
        dialogueText.text = showText;
        showTextSlowly = false;
    }
}
