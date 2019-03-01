using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
public class RebirthTalentItemCpt : PopupReplyView
{
    public Image ivTalentIcon;
    public Text tvTitle;
    public GameDataCpt gameDataCpt;
    public TalentInfoBean talentInfoBean;
    public RebirthTalentItemBean rebirthTalentItemBean;
    public override void ClosePopup()
    {

    }

    public override void OpenPopup()
    {
        string nameStr = "???";
        string contentStr = "???";

        if (talentInfoBean.unlock_level > gameDataCpt.userData.userAchievement.maxUserGoodsLevel)
        {
 
        }
        else
        {
            if (talentInfoBean != null)
            {
                nameStr = talentInfoBean.name;
                contentStr = talentInfoBean.content;
            }
            if (rebirthTalentItemBean == null)
            {
      
            }
            else
            {
   
            }
        }
        infoPopupView.SetInfoData(ivTalentIcon.sprite, nameStr, "remark","price", contentStr, "other");
    }

    public void SetData(TalentInfoBean talentInfoBean, RebirthTalentItemBean rebirthTalentItemBean)
    {
        this.talentInfoBean = talentInfoBean;
        this.rebirthTalentItemBean = rebirthTalentItemBean;
        if (gameDataCpt == null)
            return;
        if (ivTalentIcon == null)
            return;
        if (tvTitle == null)
            return;
        string iconKeyStr = "";
        string titleStr = "";

        if (talentInfoBean.unlock_level > gameDataCpt.userData.userAchievement.maxUserGoodsLevel)
        {
            iconKeyStr = "unlock";
            titleStr = "???";
        }
        else
        {
            iconKeyStr = talentInfoBean.icon_key;
            if (rebirthTalentItemBean == null)
            {
                titleStr = "Level" + " " + "0";
            }
            else
            {
                titleStr = "Level" + " " + rebirthTalentItemBean.talent_level;
            }
        }
        ivTalentIcon.sprite = gameDataCpt.GetIconByKey(iconKeyStr);
        tvTitle.text = titleStr;
    }
}