using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : BlockButton
{
    public override void OnValueChanged(bool active)
    {
        GameObject[] restorableBlocks = GameObject.FindGameObjectsWithTag("RestoreableBlock");

        foreach (GameObject restorableBlock in restorableBlocks)
        {
            restorableBlock.GetComponent<Collider>().enabled = true;
        }

        if (!active)
        {
            return;
        }
        Gamemaster.Instance.GetLevelEditor().SetBlockType(blockId);
    }
}
