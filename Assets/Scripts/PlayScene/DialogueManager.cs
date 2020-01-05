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
    [SerializeField] private bool forceXboxDialogue = false;
    private Player player;
    private Advisor advisor;
    private int dialogueIndex = 0;
    private DialogueLine[] lines;
    private DialogueLine currentLine;
    private bool shouldStartDialogue = true;
    private bool hasMoreText;
    private PlayerInputHandler inputHandler;

    void Start()
    {
        player = (Player)participants[0];
        advisor = (Advisor)participants[1];
        if (Gamemaster.Instance.GetLevelType() == LevelType.Main)
        {
            bool usingXbox = forceXboxDialogue ? true : Gamemaster.Instance.GetPlayer().GetComponent<PlayerInputHandler>().IsUsingXbox;
            lines = DialogueParser.GetDialogueLines(Gamemaster.Instance.GetLevelId() * (usingXbox ? -1 : 1));
            if (lines == null)
            {
                DisableAdvisor();
                return;
            }
        }
        else
        {
            DisableAdvisor();
            return;
        }
        inputHandler = Gamemaster.Instance.GetPlayer().GetComponent<PlayerInputHandler>();
    }

    private void DisableAdvisor()
    {
        participants[1].gameObject.SetActive(false);
    }

    private void StartDialogue()
    {
        shouldStartDialogue = false;
        inputHandler.IsInDialogue = true;
        advisor.LookAtOther(player);
        player.LookAtOther(advisor);
        advisor.DisableThoughtBubble();
        dialogueIndex = 0;
        currentLine = lines[dialogueIndex];
        participants[(int)currentLine.Speaker].StartLine(currentLine.Dialogue);
    }

    private void EndDialogue()
    {
        inputHandler.IsInDialogue = false;
        advisor.LookAtOriginalDirection();
        player.ResetAnimationState();
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
                    EndDialogue();
                }
            }
        }

    }

    public void ResetDialogue()
    {
        shouldStartDialogue = true;
    }
}
