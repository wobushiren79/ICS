using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameMenuCpt : BaseUIComponent, DialogView.IDialogCallBack
{

    //显示-名字
    public Text tvName;
    //显示-实时分数
    public Text tvScore;
    //显示-增量
    public Text tvGrow;
    //显示-标题
    public Text tvTitle;

    //按钮-商店
    public Button btStore;
    public Text tvStore;
    //按钮-技能
    public Button btSkill;
    public Text tvSill;
    //按钮-成就
    public Button btAchievement;
    public Text tvAchievement;
    //按钮-保存并退出
    public Button btSaveAndExit;
    public Text tvSaveAndExit;
    //按钮-重生
    public Button btRebirth;
    //按钮-转盘
    public Button btTurntable;
    //按钮-油
    public Button btOil;


    //数据管理
    public GameDataCpt gameDataCpt;
    //提示框
    public GameToastCpt gameToastCpt;
    //提示框
    public DialogManager dialogManager;
    /// <summary>
    /// 初始化
    /// </summary>
    private void Start()
    {
        //初始化按钮点击事件
        if (btStore != null)
            btStore.onClick.AddListener(BTStoreOnClick);
        if (btSkill != null)
            btSkill.onClick.AddListener(BTSkillOnClick);
        if (btAchievement != null)
            btAchievement.onClick.AddListener(BTAchievementOnClick);
        if (btSaveAndExit != null)
            btSaveAndExit.onClick.AddListener(BTSaveAndExitOnClick);
        if (btRebirth != null)
            btRebirth.onClick.AddListener(BTRebirthOnClick);
        if (btTurntable != null)
            btTurntable.onClick.AddListener(BTTurntableOnClick);
        if (btOil != null)
            btOil.onClick.AddListener(BTOilOnClick);
        //初始化数据
        if (tvStore != null)
            tvStore.text = GameCommonInfo.GetTextById(32);
        if (tvSill != null)
            tvSill.text = GameCommonInfo.GetTextById(33);
        if (tvAchievement != null)
            tvAchievement.text = GameCommonInfo.GetTextById(34);
        if (tvSaveAndExit != null)
            tvSaveAndExit.text = GameCommonInfo.GetTextById(35);
        if (tvTitle != null)
            tvTitle.text = GameCommonInfo.GetTextById(40);
    }

    private void Update()
    {
        //实时更新数据
        UpdateUIData();
    }


    /// <summary>
    /// 商店按钮点击
    /// </summary>
    public void BTStoreOnClick()
    {
        uiManager.OpenUIAndCloseOtherByName("GameStore");
    }

    /// <summary>
    /// 技能按钮点击
    /// </summary>
    public void BTSkillOnClick()
    {
        uiManager.OpenUIAndCloseOtherByName("GameSkill");
    }

    /// <summary>
    /// 成就按钮点击
    /// </summary>
    public void BTAchievementOnClick()
    {
        uiManager.OpenUIAndCloseOtherByName("GameAchievement");
    }

    /// <summary>
    /// 辣椒油点击
    /// </summary>
    public void BTOilOnClick()
    {
        uiManager.OpenUIAndCloseOtherByName("GameOil");
    }
    /// <summary>
    /// 保存并退出按钮
    /// </summary>
    public void BTSaveAndExitOnClick()
    {
        gameDataCpt.SaveUserData();
        SceneUtil.SceneChange("MainScene");
    }

    /// <summary>
    /// 重生按钮
    /// </summary>
    public void BTRebirthOnClick()
    {
        if (gameToastCpt!=null&& gameDataCpt.userData.userScore < 1e8)
        {
            gameToastCpt.ToastHint(GameCommonInfo.GetTextById(87));
            return;
        }
        if (dialogManager != null)
        {
            DialogBean dialogBean = new DialogBean();
            dialogBean.title = GameCommonInfo.GetTextById(86);
            dialogBean.content = GameCommonInfo.GetTextById(85);
            dialogBean.submitStr = GameCommonInfo.GetTextById(57);
            dialogBean.cancelStr = GameCommonInfo.GetTextById(58);
            dialogManager.CreateDialog(0,this, dialogBean);
        }
    }

    /// <summary>
    /// 转盘按钮
    /// </summary>
    public void BTTurntableOnClick()
    {
        uiManager.OpenUIAndCloseOtherByName("GameTurntable");
    }


    /// <summary>
    /// //实时更新数据
    /// </summary>
    public void UpdateUIData()
    {
        if (gameDataCpt == null)
            return;
        if (tvScore != null)
        {
            string outNumberStr;
            UnitUtil.UnitEnum outUnit;
            UnitUtil.DoubleToStrUnit(gameDataCpt.userData.userScore,out outNumberStr,out outUnit);
            tvScore.text = outNumberStr+" " + GameCommonInfo.GetUnitStr(outUnit);
        }
        if (tvGrow != null)
        {
            string outNumberStr;
            UnitUtil.UnitEnum outUnit;
            UnitUtil.DoubleToStrUnitKeepNumber(gameDataCpt.userData.userGrow*gameDataCpt.userData.userTimes, 2, out outNumberStr, out outUnit);
            tvGrow.text = GameCommonInfo.GetTextById(39)+":" + outNumberStr +" "+GameCommonInfo.GetUnitStr(outUnit);
        }
        if (tvName != null)
            tvName.text = gameDataCpt.userData.userName;
    }

    #region 重生弹窗回调
    public void Cancel(DialogView dialogView)
    {

    }

    public void Submit(DialogView dialogView)
    {
        gameDataCpt.SaveUserData();
        SceneUtil.SceneChange("RebirthScene");
    }
    #endregion
}
