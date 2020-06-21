using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RespawnBlock_Data : Block_Data
{
    public override BlockType BlockType => BlockType.Respawn;

    private float respawnTime;

    public void SetRespawnTime(float respawnTime)
    {
        this.respawnTime = respawnTime;
    }

    public float GetRespawnTime()
    {
        return respawnTime;
    }
}
