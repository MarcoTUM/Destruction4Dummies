using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParticipant : MonoBehaviour
{
    [SerializeField] protected DialogueCanvas dialogueCanvas;

    public virtual void StartLine(string line)
    {
        dialogueCanvas.PrintLine(line);
    }

    public bool ContinueLine()
    {
        return dialogueCanvas.NextPage();
    }

    public void StopLine()
    {
        dialogueCanvas.DisableOpenPanels();
    }
}
