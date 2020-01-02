using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvisorDialogueCanvas : DialogueCanvas
{
    [SerializeField] private RectTransform thinkingBubble;
    [SerializeField] private bool forceXboxDialogue;
    [SerializeField] private RectTransform xboxSymbol, spaceBarSymbol;
    private bool isUsingXbox;

    protected override void Start()
    {
        base.Start();
        this.isUsingXbox = forceXboxDialogue ? true : Gamemaster.Instance.GetPlayer().GetComponent<PlayerInputHandler>().IsUsingXbox;
    }

    /// <summary>
    /// Signalizes the player he can interact with Advisor
    /// </summary>
    public void EnableToughtBubble()
    {
        thinkingBubble.gameObject.SetActive(true);
        xboxSymbol.gameObject.SetActive(isUsingXbox);
        spaceBarSymbol.gameObject.SetActive(!isUsingXbox);
    }

    public void DisableThoughtBubble()
    {
        thinkingBubble.gameObject.SetActive(false);
    }

    public override void DisableOpenPanels()
    {
        base.DisableOpenPanels();
        DisableThoughtBubble();
    }
}
