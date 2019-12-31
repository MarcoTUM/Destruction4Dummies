using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Speaker { Player, Advisor };

public struct DialogueLine
{
    public Speaker Speaker;
    public string Dialogue;
    public DialogueLine(Speaker speaker, string dialogue)
    {
        this.Speaker = speaker;
        this.Dialogue = dialogue;
    }
}

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueParticipant[] participants = new DialogueParticipant[2];//0=Player, 1=Advisor
    private int dialogueIndex = 0;
    private DialogueLine[] lines;
    private DialogueLine currentLine;
    private bool shouldStartDialogue = true;
    private bool hasMoreText;
    private PlayerInputHandler inputHandler;

    void Start()
    {
        if (Gamemaster.Instance.GetLevelType() == LevelType.Main)
        {
            Debug.Log(Gamemaster.Instance.GetLevelId());
            lines = DialogueParser.GetDialogueLines(Gamemaster.Instance.GetLevelId());
            if (lines == null)
                DisableAdvisor();
        }
        else
            DisableAdvisor();
        inputHandler = Gamemaster.Instance.GetPlayer().GetComponent<PlayerInputHandler>();
    }

    private void DisableAdvisor()
    {
        participants[1].gameObject.SetActive(false);
    }

    public void StartDialogue()
    {
        shouldStartDialogue = false;
        inputHandler.IsInDialogue = true;
        ((Advisor)participants[1]).DisableThoughtBubble();
        dialogueIndex = 0;
        currentLine = lines[dialogueIndex];
        participants[(int)currentLine.Speaker].StartLine(currentLine.Dialogue);
    }

    /// <summary>
    /// Handles when player presses interaction(currentyl jump) button 
    /// Either starts talking to Advisor -> opening speechBubble
    /// or progressing the dialogue with the next page/closing the speechBubble
    /// </summary>
    public void HandlePlayerInteraction()
    {
        if (shouldStartDialogue)
        {
            StartDialogue();
        }
        else
        {
            hasMoreText = participants[(int)currentLine.Speaker].ContinueLine();
            if (!hasMoreText)
            {
                if (dialogueIndex < lines.Length - 1)
                {
                    currentLine = lines[++dialogueIndex];
                    participants[(int)currentLine.Speaker].StartLine(currentLine.Dialogue);
                }
                else
                {
                    inputHandler.IsInDialogue = false;
                }
            }
        }

    }

    public void ResetDialogue()
    {
        shouldStartDialogue = true;
    }
}
