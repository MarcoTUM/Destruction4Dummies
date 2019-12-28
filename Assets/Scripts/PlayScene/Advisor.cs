using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AdvisorAnimator))]
public class Advisor : MonoBehaviour
{
    private AdvisorAnimator animator;
    private AdvisorDialogueCanvas dialogueCanvas;

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

    public void Initialize()
    {
        if (Gamemaster.Instance.GetLevelType() != LevelType.Main)
            return;
        Vector2Int startPlatformCoord = Gamemaster.Instance.GetLevel().GetLevelData().StartPlatformCoordinates;
        Debug.Log(startPlatformCoord.y);
        this.transform.position = new Vector3(startPlatformCoord.x - Block_Data.BlockSize, startPlatformCoord.y + Block_Data.BlockSize/2f, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            animator.StopTalking();
    }

    public void HandlePlayerDeath()
    {
        animator.BeDisappointed();
    }

    public void HandlePlayerInteraction()
    {
        Debug.Log("Interact");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagDictionary.Player)
        {
            other.GetComponent<Player>().IsInteractingWithAdvisor = true;
            dialogueCanvas.EnableInteraction();
            Debug.Log("Enter: " + other.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == TagDictionary.Player)
        {
            other.GetComponent<Player>().IsInteractingWithAdvisor = false;
            dialogueCanvas.DisableInteraction();
            Debug.Log("Exit: " + other.gameObject.name);
        }
    }
}
