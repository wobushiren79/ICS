using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public class GameSkillsDetailsItem : PopupReplyView
{
    public Image ivIcon;
    public LevelSkillsBean levelSkillsBean;

    public void SetData(LevelSkillsBean levelSkillsBean,Sprite iconSp)
    {
        this.levelSkillsBean = levelSkillsBean;
        if (ivIcon != null)
            ivIcon.sprite = iconSp;
    }

    public override void ClosePopup()
    {
 
    }

    public override void OpenPopup()
    {
        if (ivIcon == null|| levelSkillsBean==null)
            return;
        string nameStr = levelSkillsBean.name;
        string remarkStr = "remark";
        string priceStr = levelSkillsBean.price+"";
        string descriptionStr = levelSkillsBean.introduction;
        infoPopupView.SetInfoData(ivIcon.sprite, nameStr, remarkStr, priceStr, descriptionStr,null);
    }
}