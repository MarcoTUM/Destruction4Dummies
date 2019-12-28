using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AdvisorAnimator : MonoBehaviour
{
    #region AnimatorHashes
    //States:
    private static readonly int idleState = Animator.StringToHash("Idle");
    private static readonly int talkState1 = Animator.StringToHash("Talking1");
    private static readonly int talkState2 = Animator.StringToHash("Talking2");
    private static readonly int disappointedState = Animator.StringToHash("Disappointed");

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
        animator.SetBool(talkingParam, true);
        animator.Play(talkState1);
    }

    public void StopTalking()
    {
        animator.SetBool(talkingParam, false);
    }

    public void BeDisappointed()
    {
        animator.Play(disappointedState);
    }
}
