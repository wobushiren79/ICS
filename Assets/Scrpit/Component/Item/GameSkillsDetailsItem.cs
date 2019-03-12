using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class GameSkillsDetailsItem : PopupReplyView
{
    public Image ivIcon;
    public Image ivBorder;
    public Image ivBackground;
    public Button btSubmit;
    public LevelSkillsBean levelSkillsBean;
    public LevelScenesBean levelScenesBean;
    public GameToastCpt gameToastCpt;
    public GameDataCpt gameDataCpt;
    public GameAchievementCpt gameAchievementCpt;
    private bool hasSkills = false;


    public Color noSkillColor = new Color(0.3f, 0.3f, 0.3f);
    public Color hasMoneyColor = new Color(0f,1f,0);
    public Color noMoneyColor = new Color(1f, 1f,1f,0f);

    private void Update()
    {
        if (hasSkills)
        {
            ivBackground.color = noMoneyColor;
        }
        else
        {
            if (levelSkillsBean.price > gameDataCpt.userData.userScore)
            {
                ivBackground.color = noMoneyColor;
            }
            else
            {
                ivBackground.color = hasMoneyColor;
            }
        }
    }

    public virtual void SetData(LevelSkillsBean levelSkillsBean, LevelScenesBean levelScenesBean)
    {
        this.levelScenesBean = levelScenesBean;
        this.levelSkillsBean = levelSkillsBean;
        if (gameDataCpt == null)
            return;
        if (ivIcon != null)
            ivIcon.sprite = gameDataCpt.GetIconByKey(levelSkillsBean.icon_key);
        hasSkills = gameDataCpt.HasSkillsById(levelSkillsBean.id);
        if (hasSkills)
        {

        }
        else
        {
            ivBorder.color = noSkillColor;
            ivIcon.color = noSkillColor;
        }
        btSubmit.onClick.RemoveAllListeners();
        btSubmit.onClick.AddListener(BTSkillsBuyOnClick);
    }

    /// <summary>
    /// 购买按钮
    /// </summary>
    public void BTSkillsBuyOnClick()
    {
        if (levelSkillsBean == null)
            return;
        if (!hasSkills)
        {
            UserItemLevelBean userLevelData = gameDataCpt.GetUserItemLevelDataByLevel(levelSkillsBean.level);
            if (userLevelData == null || userLevelData.goodsNumber == 0)
            {
                gameToastCpt.ToastHint(GameCommonInfo.GetTextById(51) + " " + levelScenesBean.goods_name);
                return;
            }
            if (levelSkillsBean.add_number != 0)
            {
                bool canAdd = gameDataCpt.HasSpaceToAddGoodsByLevel(levelSkillsBean.level, levelSkillsBean.add_number);
                if (!canAdd)
                {
                    gameToastCpt.ToastHint(GameCommonInfo.GetTextById(45));
                    return;
                }
            }
            bool hasRemove = gameDataCpt.RemoveScore(levelSkillsBean.price);
            if (!hasRemove)
            {
                gameToastCpt.ToastHint(GameCommonInfo.GetTextById(44));
                return;
            }
            //设置数据
            if (levelSkillsBean.add_number != 0)
            {
                gameDataCpt.AddLevelGoods(levelSkillsBean.level, levelSkillsBean.add_number);
            }
            if (levelSkillsBean.add_grow != 0)
            {
                userLevelData.itemGrow += levelSkillsBean.add_grow;
            }
            if (levelSkillsBean.add_times != 0)
            {
                userLevelData.itemTimes += levelSkillsBean.add_times;
            }
            if (levelSkillsBean.add_user_times != 0)
            {
                gameDataCpt.userData.userTimes += levelSkillsBean.add_user_times;
            }

            gameDataCpt.userData.listSkillsData.Add(levelSkillsBean.id);
            gameDataCpt.userData.listSkillsData.Sort();
            //添加成就
            if (gameDataCpt.userData.userAchievement == null)
            {
                gameDataCpt.userData.userAchievement = new AchievementBean();
            }
            if (gameDataCpt.userData.userAchievement.unlockSkillsList==null)
            {
                gameDataCpt.userData.userAchievement.unlockSkillsList = new System.Collections.Generic.List<long>();
            }
            gameDataCpt.userData.userAchievement.unlockSkillsList.Add(levelSkillsBean.id);
            gameDataCpt.userData.userAchievement.unlockSkillsList=gameDataCpt.userData.userAchievement.unlockSkillsList.Distinct().ToList(); ;
            gameDataCpt.userData.userAchievement.unlockSkillsList.Sort();
            if (gameAchievementCpt != null)
            {
                gameAchievementCpt.UpdateUnlockSkillsData();
            }
            ivBorder.color = new Color(1, 1, 1);
            ivIcon.color = new Color(1, 1, 1);

            hasSkills = true;

            gameDataCpt.RefreshData();
            OpenPopup();
        }

        transform.DOKill();
        transform.localScale = new Vector3(1, 1, 1);
        transform.localRotation = new Quaternion();
        transform.DOPunchScale(new Vector3(0.5f, 0.5f, 1), 1);
        transform.DOShakeRotation(1, new Vector3(0, 0, 360));
    }

    public override void ClosePopup()
    {

    }

    public override void OpenPopup()
    {
        if (ivIcon == null || levelSkillsBean == null)
            return;
        string nameStr = levelSkillsBean.name;
        string remarkStr = "";
        string otherStr = "";
        string numberStr = "";
        if (levelSkillsBean.add_user_times != 0)
        {

            remarkStr += "【" + GameCommonInfo.GetTextById(52) + "】";
            otherStr += "➤" + GameCommonInfo.GetTextById(40) + GameCommonInfo.GetTextById(42) + GameCommonInfo.GetTextById(52) + GameCommonInfo.GetTextById(49)+levelSkillsBean.add_user_times*100 +"%" + "\n";
        }
        if (levelSkillsBean.add_grow != 0)
        {
            numberStr = GameCommonInfo.GetPriceStr(levelSkillsBean.add_grow,2);
            remarkStr += "【" + GameCommonInfo.GetTextById(46) + "】";
            if (levelScenesBean.level == 0)
            {
                otherStr += "➤" + GameCommonInfo.GetTextById(53) + levelScenesBean.goods_name+ GameCommonInfo.GetTextById(49) + numberStr + GameCommonInfo.GetTextById(40) + "\n";
            }
            else
            {
                otherStr += "➤" + GameCommonInfo.GetTextById(41) + levelScenesBean.goods_name + GameCommonInfo.GetTextById(39) + GameCommonInfo.GetTextById(49) + numberStr + GameCommonInfo.GetTextById(40) + "\n";
            }
        }
        if (levelSkillsBean.add_number != 0)
        {
            numberStr = GameCommonInfo.GetPriceStr(levelSkillsBean.add_number);
            remarkStr += "【" + GameCommonInfo.GetTextById(48) + "】";
            otherStr += "➤" + levelScenesBean.goods_name + GameCommonInfo.GetTextById(48) + GameCommonInfo.GetTextById(49) + numberStr + GameCommonInfo.GetTextById(43) + "\n";
        }
        if (levelSkillsBean.add_times != 0)
        {
            remarkStr += "【" + GameCommonInfo.GetTextById(47) + "】";
            otherStr += "➤" + levelScenesBean.goods_name + GameCommonInfo.GetTextById(50) + GameCommonInfo.GetTextById(42) + GameCommonInfo.GetTextById(47) + GameCommonInfo.GetTextById(49) + levelSkillsBean.add_times * 100 + "%" + "\n";
        }
        string priceStr = null;
        if (hasSkills)
        {

        }
        else
        {
            priceStr =GameCommonInfo.GetPriceStr(levelSkillsBean.price);
        }

        string descriptionStr = levelSkillsBean.introduction;
        infoPopupView.SetInfoData(ivIcon.sprite, nameStr, remarkStr, priceStr, descriptionStr, otherStr);
    }
}