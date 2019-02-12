using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameAchievementCpt : BaseUIComponent
{
    public Button btBack;
    public Text tvBack;
    public RectTransform contentTF;

    public GameAchievementGeneralCpt achievementGeneralCpt;
    public GameAchievementSkillCpt achievementSkillCpt;

    //标题
    public Text tvTitle;

    private void Start()
    {
        if (btBack != null)
            btBack.onClick.AddListener(BTBack);
        if (tvBack != null)
            tvBack.text = GameCommonInfo.GetTextById(36);
        if (tvTitle != null)
            tvTitle.text = GameCommonInfo.GetTextById(34);
        InitData();
    }
    public override void OpenUI()
    {
        base.OpenUI();
        InitData();
    }

    public void InitData()
    {
        if (achievementGeneralCpt)
            achievementGeneralCpt.RefreshData();
        if (achievementSkillCpt)
            achievementSkillCpt.RefreshData();
        GameUtil.RefreshRectViewHight(contentTF, true);
    }

    /// <summary>
    /// 返回按钮点击
    /// </summary>
    public void BTBack()
    {
        uiManager.OpenUIAndCloseOtherByName("GameMenu");
    }
}
