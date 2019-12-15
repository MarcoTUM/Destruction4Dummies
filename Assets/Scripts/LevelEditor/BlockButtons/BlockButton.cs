using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Toggle))]
public class BlockButton : MonoBehaviour
{
    [SerializeField] protected int blockId; //only used when SetBlockType(id) is used
    [SerializeField] protected Texture2D tex;
    [SerializeField] protected Text descriptionText;

    protected virtual void Start()
    {
        if (tex == null)
            return;
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        Image[] images = this.GetComponentsInChildren<Image>();
        this.GetComponent<Image>().sprite = sprite;
    }


    /// <summary>
    /// Sets BlockType when you click on BlockButton
    /// </summary>
    /// <param name="active"></param>
    public virtual void OnValueChanged(bool active)
    {
        if (!active)
        {
            return;
        }
        Gamemaster.Instance.GetLevelEditor().SetBlockType(blockId);
    }


    #region BlockDescription
    public virtual void OnPointerEnter()
    {
        descriptionText.text = BlockText();
    }

    public virtual void OnPointerExit()
    {
        descriptionText.text = "";
    }

    /// <summary>
    /// Override with blockSpecific InformationText
    /// </summary>
    /// <returns></returns>
    protected virtual string BlockText()
    {
        return "<b>Block:</b>\nI am a block!";
    }
    #endregion
}
