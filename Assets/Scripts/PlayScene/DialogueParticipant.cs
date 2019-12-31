using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParticipant : MonoBehaviour
{
    [SerializeField] protected DialogueCanvas dialogueCanvas;
    [SerializeField] protected Transform myModel;
    private Quaternion ogRotation;
    public Transform GetModel()
    {
        return myModel;
    }

    public virtual void LookAtOther(DialogueParticipant otherPart)
    {
        Vector3 otherPos = otherPart.GetModel().position;
        otherPos.y = myModel.position.y;
        ogRotation = myModel.rotation;
        myModel.LookAt(otherPos);
    }

    public virtual void LookAtOriginalDirection()
    {
        myModel.rotation = ogRotation;
    }

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
