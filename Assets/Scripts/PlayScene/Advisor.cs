using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AdvisorAnimator))]
public class Advisor : MonoBehaviour
{
    private AdvisorAnimator animator;
    private AdvisorDialogueCanvas dialogueCanvas;
    private bool isTalking = false;

    private void Awake()
    {
        animator = this.GetComponent<AdvisorAnimator>();
        dialogueCanvas = this.GetComponentInChildren<AdvisorDialogueCanvas>();
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

    /// <summary>
    /// Handles when player presses interaction(currentyl jump) button 
    /// Either starts talking to Advisor -> opening speechBubble
    /// or progressing the dialogue with the next page/closing the speechBubble
    /// </summary>
    public void HandlePlayerInteraction()
    {
        
        if (!isTalking)
        {
            isTalking = true;
            animator.StartTalking();
            dialogueCanvas.ShowDialogue();
        }
        else
        {
            bool hadNextPage = dialogueCanvas.NextPage();
            if (!hadNextPage)
            {
                dialogueCanvas.EnableToughtBubble();
                animator.StopTalking();
                isTalking = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagDictionary.Player)
        {
            other.GetComponent<Player>().IsInteractingWithAdvisor = true;
            dialogueCanvas.EnableToughtBubble();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == TagDictionary.Player)
        {
            other.GetComponent<Player>().IsInteractingWithAdvisor = false;
            dialogueCanvas.DisableInteractionBubble();
            animator.StopTalking();
            isTalking = false;
        }
    }
}
