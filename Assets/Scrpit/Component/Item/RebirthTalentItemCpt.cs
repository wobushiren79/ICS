using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class RebirthTalentItemCpt : PopupReplyView
{
    public Image ivTalentIcon;
    public Text tvTitle;
    public Button btTalent;
    public ParticleSystem psTalent;

    public GameToastCpt gameToastCpt;
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
        string remark = GameCommonInfo.GetTextById(79);
        string priceStr = "???";
        string otherStr = "";
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
            priceStr = GetTalentPrice(talentInfoBean.price, rebirthTalentItemBean.talent_level) + "";
            otherStr += ("◆" + talentInfoBean.other + talentInfoBean.add_number * 100 + "%");
            otherStr += "\n";
            otherStr += ("◆" + GameCommonInfo.GetTextById(80) + rebirthTalentItemBean.talent_level);
            otherStr += (" " + GameCommonInfo.GetTextById(81) + rebirthTalentItemBean.total_add * 100 + "%");
        }
        infoPopupView.SetInfoData(ivTalentIcon.sprite, nameStr, remark, priceStr, contentStr, otherStr);
    }

    public void SetData(TalentInfoBean talentBean, RebirthTalentItemBean rebirthBean)
    {
        this.talentInfoBean = talentBean;
        this.rebirthTalentItemBean = rebirthBean;
        if (rebirthTalentItemBean == null)
        {
            this.rebirthTalentItemBean = new RebirthTalentItemBean();
            this.rebirthTalentItemBean.talent_id = talentBean.id;
            this.rebirthTalentItemBean.add_type = talentBean.add_type;
            gameDataCpt.userData.rebirthData.listRebirthTalentData.Add(this.rebirthTalentItemBean);
        }
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
            titleStr = GameCommonInfo.GetTextById(82) + " " + this.rebirthTalentItemBean.talent_level;
        }
        ivTalentIcon.sprite = gameDataCpt.GetIconByKey(iconKeyStr);
        tvTitle.text = titleStr;

        if (btTalent != null) {
            btTalent.onClick.RemoveAllListeners();
            btTalent.onClick.AddListener(delegate ()
            {
                BTTalentOnClick(this.talentInfoBean, this.rebirthTalentItemBean);
            });
        }   
    }

    /// <summary>
    /// 天赋点击处理时间
    /// </summary>
    /// <param name="talentBean"></param>
    /// <param name="rebirthBean"></param>
    private void BTTalentOnClick(TalentInfoBean talentBean, RebirthTalentItemBean rebirthBean)
    {
        if (talentBean == null || rebirthBean == null)
            return;
        if (talentInfoBean.unlock_level > gameDataCpt.userData.userAchievement.maxUserGoodsLevel)
        {
            gameToastCpt.ToastHint(GameCommonInfo.GetTextById(83));
            return;
        }
        transform.DOKill();
        transform.localScale = new Vector3(1, 1, 1);
        transform.DOScale(new Vector3(0.8f, 0.8f), 0.3f).From();
        double price = GetTalentPrice(talentBean.price, rebirthBean.talent_level);
        if (gameDataCpt.userData.rebirthData.rebirthChili - price < 0)
        {
            gameToastCpt.ToastHint(GameCommonInfo.GetTextById(84));
            return;
        }
        gameDataCpt.userData.rebirthData.rebirthChili -= price;
        rebirthTalentItemBean.talent_level += 1;
        rebirthTalentItemBean.total_add += talentBean.add_number;

        SetData(talentBean, rebirthBean);
        OpenPopup();
        if (psTalent != null)
            psTalent.Play();
    }

    /// <summary>
    /// 获取解锁金额
    /// </summary>
    /// <param name="startPrice"></param>
    /// <param name="talentLevel"></param>
    /// <returns></returns>
    private double GetTalentPrice(double startPrice, int talentLevel)
    {
        if (talentLevel == 0)
        {
            return startPrice;
        }
        else
        {
            return startPrice * talentLevel  * 2;
        }
     
    }
}