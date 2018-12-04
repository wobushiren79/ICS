﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameMenuCpt : BaseUIComponent
{
    //按钮-商店
    public Button btStore;
    //按钮-技能
    public Button btSkill;
    //按钮-成就
    public Button btAchievement;
    //按钮-保存并退出
    public Button btSaveAndExit;

    /// <summary>
    /// 初始化
    /// </summary>
    private void Start () {
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

    }
}
