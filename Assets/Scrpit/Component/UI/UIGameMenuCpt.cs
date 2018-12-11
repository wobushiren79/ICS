﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameMenuCpt : BaseUIComponent
{

    //显示-名字
    public Text tvName;
    //显示-实时分数
    public Text tvScore;
    //显示-增量
    public Text tvGrow;

    //按钮-商店
    public Button btStore;
    //按钮-技能
    public Button btSkill;
    //按钮-成就
    public Button btAchievement;
    //按钮-保存并退出
    public Button btSaveAndExit;

    //数据管理
    public GameDataCpt gameDataCpt;
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
    /// 保存并退出按钮
    /// </summary>
    public void BTSaveAndExitOnClick()
    {
        gameDataCpt.SaveUserData();
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
            tvScore.text = outNumberStr+" "+(int)outUnit;
        }
        if (tvGrow != null)
        {
            string outNumberStr;
            UnitUtil.UnitEnum outUnit;
            UnitUtil.DoubleToStrUnitKeepNumber(gameDataCpt.userData.userGrow, 2, out outNumberStr, out outUnit);
            tvGrow.text = "per second:" + outNumberStr +" "+ (int)outUnit;
        }
        if (tvName != null)
            tvName.text = gameDataCpt.userData.userName;
    }
}
