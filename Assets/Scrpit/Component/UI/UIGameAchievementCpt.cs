using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameAchievementCpt : BaseUIComponent
{
    //返回按钮
    public Button btBack;

    private void Start()
    {
        if (btBack != null)
            btBack.onClick.AddListener(BTBack);
    }

    /// <summary>
    /// 返回按钮点击
    /// </summary>
    public void BTBack()
    {
        uiManager.OpenUIAndCloseOtherByName("GameMenu");
    }
}
