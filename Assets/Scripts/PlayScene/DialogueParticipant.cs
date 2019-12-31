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

    public virtual bool ContinueLine()
    {
        bool hasNextPage = dialogueCanvas.NextPage();
        if (!hasNextPage)
            StopLine();
        return hasNextPage;
    }

    public virtual void StopLine()
    {
        dialogueCanvas.DisableOpenPanels();
    }
}
