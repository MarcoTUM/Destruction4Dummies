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
        Player playerScript = player.GetComponent<Player>();
        if (playerScript.IsOnGoal)
            return;
        float goalX = Gamemaster.Instance.GetLevel().GetLevelData().GoalPlatformCoordinates.x * Block_Data.BlockSize;
        float goalMargin = 1.5f * Block_Data.BlockSize;
        float modelXPos = playerScript.ShoesRenderer.bounds.center.x;
        float modelOffset = playerScript.ShoesRenderer.bounds.extents.x / 2f;
        if (player.transform.position.y > this.transform.position.y 
            && modelXPos + modelOffset >= goalX - goalMargin 
            && modelXPos - modelOffset <= goalX + goalMargin)
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
    #region ContinousHandling
    protected void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == TagDictionary.Player)
        {
            OnTouch(collider.gameObject);
        }
    }
    #endregion
}
