using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AdvisorAnimator : MonoBehaviour
{
    #region AnimatorHashes
    //Trigger:
    private static readonly int[] talkingTriggers = new int[2] { Animator.StringToHash("TalkingTrigger1"), Animator.StringToHash("TalkingTrigger2") };
    private static readonly int disappointedTrigger = Animator.StringToHash("DisappointedTrigger");
    private static readonly int idleTrigger = Animator.StringToHash("IdleTrigger");

    //Params:
    private static readonly int talkingParam = Animator.StringToHash("Talking");
    #endregion

    private Animator animator;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    public void StartTalking()
    {
        int random = UnityEngine.Random.Range(0, talkingTriggers.Length);
        for(int i=0; i<talkingTriggers.Length; i++)
        {
            if (i == random)
                animator.SetTrigger(talkingTriggers[i]);
            else
                animator.ResetTrigger(talkingTriggers[i]);
        }
        animator.SetBool(talkingParam, true);
    }

    public void StopTalking()
    {
        animator.SetTrigger(idleTrigger);
        animator.SetBool(talkingParam, false);
    }

    public void BeDisappointed()
    {
        animator.SetTrigger(disappointedTrigger);
    }
}
