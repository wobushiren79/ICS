using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainStartCpt : BaseUIComponent {
    
    //退出按钮
    public Button btExit;
    public Text tvExit;
    //开始按钮
    public Button btStart;
    public Text tvStart;

    private void Start()
    {
        if (btExit != null)
            btExit.onClick.AddListener(BTExitOnClick);
        if (btStart != null)
            btStart.onClick.AddListener(BTStartOnClick);

        if (tvStart != null)
            tvStart.text = GameCommonInfo.GetTextById(1);
        if (tvExit != null)
            tvExit.text = GameCommonInfo.GetTextById(2);
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
