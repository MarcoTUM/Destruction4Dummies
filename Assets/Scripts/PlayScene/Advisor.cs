using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(AdvisorAnimator))]
public class Advisor : DialogueParticipant
{
    private AdvisorAnimator animator;
    [SerializeField] private DialogueManager dialogueManager;
    private AdvisorDialogueCanvas advisorDialogueCanvas;

    private void Awake()
    {
        animator = this.GetComponent<AdvisorAnimator>();
        if (dialogueCanvas.GetType() != typeof(AdvisorDialogueCanvas))
            throw new InvalidOperationException("DialogueCanvas does not have the type of AdvisorDialogueCanvas");
        advisorDialogueCanvas = (AdvisorDialogueCanvas)dialogueCanvas;
    }

    private void Start()
    {
        if (Gamemaster.Instance.GetLevelType() != LevelType.Main)
        {
            this.gameObject.SetActive(false);
            return;
        }
    }

    public void InitializePosition()
    {
        if (Gamemaster.Instance.GetLevelType() != LevelType.Main)
            return;
        Vector2Int startPlatformCoord = Gamemaster.Instance.GetLevel().GetLevelData().StartPlatformCoordinates;
        this.transform.position = new Vector3(startPlatformCoord.x - 2 * Block_Data.BlockSize, startPlatformCoord.y + Block_Data.BlockSize/2f, 2*Block_Data.BlockSize);
    }

    /// <summary>
    /// Funny little reaction when player dies
    /// </summary>
    public void HandlePlayerDeath()
    {
        animator.BeDisappointed();
    }

    public override void StartLine(string line)
    {
        base.StartLine(line);
        advisorDialogueCanvas.DisableThoughtBubble();
    }

    public void EnableThoughtBubble()
    {
        advisorDialogueCanvas.EnableToughtBubble();
    }

    public void DisableThoughtBubble()
    {
        advisorDialogueCanvas.DisableThoughtBubble();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagDictionary.Player)
        {
            other.GetComponent<Player>().IsInteractingWithAdvisor = true;
            advisorDialogueCanvas.EnableToughtBubble();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == TagDictionary.Player)
        {
            other.GetComponent<Player>().IsInteractingWithAdvisor = false;
            advisorDialogueCanvas.DisableThoughtBubble();
            animator.StopTalking();
        }
    }
}
