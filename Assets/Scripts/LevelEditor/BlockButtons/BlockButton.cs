using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Toggle))]
public class BlockButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected int unlockValue;//=number of levels completed!
    [SerializeField] protected int blockId; //only used when SetBlockType(id) is used
    [SerializeField] protected Texture2D tex;
    [SerializeField] protected string blockName;
    [SerializeField] protected string blockDescription;

    protected Text descriptionText;
    protected static string defaultDescriptionText = null;
    protected virtual void Start()
    {
        descriptionText = GameObject.FindGameObjectWithTag(TagDictionary.BlockDescriptionText).GetComponent<Text>();
        if (defaultDescriptionText == null)
            defaultDescriptionText = descriptionText.text;

        if (tex == null)
            return;
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
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

    /// <summary>
    /// Unlocks button if enough levels are completed
    /// </summary>
    /// <returns>if unlocking was successful or not</returns>
    public bool UnlockButton()
    {
        if(Gamemaster.Instance.GetProgress() < unlockValue)
        {
            this.transform.parent.gameObject.SetActive(false);
            return false;
        }
        //Unlocking = do nothing
        return true;
    }
    #region BlockDescription
    protected virtual string BlockText()
    {
        //Replace as workaround for unity bug where linebreaks in inspector do not work
        return "<b>" + blockName + ":</b>\n" + blockDescription.Replace("<br>", "\n");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionText.text = BlockText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionText.text = defaultDescriptionText;
    }
    #endregion
}
