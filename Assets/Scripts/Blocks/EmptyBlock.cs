using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyBlock : Block
{
    Block_Data emptyBlockData = new EmptyBlock_Data();
    public override Block_Data BlockData { get => emptyBlockData; set => emptyBlockData = value; }

    #region Initialization / Destruction

    #endregion

    #region PlayerInteraction

    #endregion
}
