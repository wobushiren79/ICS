using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainStartCpt : BaseUIComponent {
    
    //退出按钮
    public Button btExit;
    //开始按钮
    public Button btStart;

    private void Start()
    {
        if (btExit != null)
            btExit.onClick.AddListener(BTExitOnClick);
        if (btStart != null)
            btStart.onClick.AddListener(BTStartOnClick);
    }
    /// <summary>
    /// 开始按钮点击
    /// </summary>
    public void BTStartOnClick()
    {
        uiManager.OpenUIAndCloseOtherByName("Data");
    }

    /// <summary>
    /// 退出按钮点击
    /// </summary>
    public void BTExitOnClick()
    {
        GameUtil.ExitGame();
    }
}
