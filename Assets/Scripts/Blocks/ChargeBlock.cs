﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBlock : Block
{
    private Block_Data chargeBlockData = new ChargeBlock_Data();
    public override Block_Data BlockData { get => chargeBlockData; set => chargeBlockData = value; }

    [SerializeField]
    private float outbreakRadius;

    public ParticleSystem forceOutbreak;

    [SerializeField]
    private float chargeTime;


    #region Initialization / Destruction
    public override void InitializeBlock(Block_Data data)
    {
        // Negative chargeTimer makes no sence
        if (chargeTime < 0)
            throw new ArgumentOutOfRangeException("chargeTime", "Negative chargeTimer makes no sence.");

        base.InitializeBlock(data);
    }

    protected override void DestroyBlock()
    {
        base.DestroyBlock();
    }

    public override void ResetBlock()
    {
        GameObject.FindObjectOfType<PlayScene>().StopForceOutbreak();
        base.ResetBlock();
    }

    #endregion

    #region PlayerInteraction

    protected override void OnTouch(GameObject player)
    {
        if (TouchedOnGoal())
            return;
        base.OnTouch(player);
        if (Gamemaster.Instance.GetPlayer().canDestroy)
        {
            GameObject.FindObjectOfType<PlayScene>().StartForceOutbreak(chargeTime, outbreakRadius, forceOutbreak);
            Gamemaster.Instance.GetPlayer().InvokeChargeBlock(chargeTime);
        }
    }

    protected override void OnTouchEnd(GameObject player)
    {
        base.OnTouchEnd(player);
    }

    #endregion

    #region helper

    protected override void SpawnDestructionEffect()
    {
        Instantiate(EffectManager.Instance.GetEffect(9), transform.position, Quaternion.identity);
    }

    #endregion
}
