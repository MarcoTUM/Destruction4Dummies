using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBlock : Block
{
    private Block_Data goalBlockData = new EmptyBlock_Data();
    public override Block_Data BlockData { get => goalBlockData; set => goalBlockData = value; }

    #region PlayerInteraction

    protected override void OnTouch(GameObject player)
    {
        if(player.transform.position.y > this.transform.position.y)
        {
            ReachedGoal();
        }
    }

    protected override void OnTouchEnd(GameObject player)
    {

    }

    private void ReachedGoal()
    {
        Gamemaster.Instance.GetPlayer().ReachGoalPlatform();
        Gamemaster.Instance.GetPlaySceneUI().OpenLevelCompleteWindow();
        if (Gamemaster.Instance.GetLevelType() == LevelType.Test)
            Gamemaster.Instance.GetLevel().GetLevelData(). IsExportable = true;
    }

    #endregion
}
