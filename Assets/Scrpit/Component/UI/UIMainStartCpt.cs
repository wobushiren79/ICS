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
    //设置按钮
    public Button btSetting;
    public Text tvSetting;

    private void Start()
    {
        if (btExit != null)
            btExit.onClick.AddListener(BTExitOnClick);
        if (btStart != null)
            btStart.onClick.AddListener(BTStartOnClick);
        if (btSetting != null)
            btSetting.onClick.AddListener(BTSettingOnClick);

        if (tvStart != null)
            tvStart.text = GameCommonInfo.GetTextById(1);
        if (tvExit != null)
            tvExit.text = GameCommonInfo.GetTextById(2);
        if(tvSetting!=null)
            tvSetting.text= GameCommonInfo.GetTextById(68);
    }


    /// <summary>
    /// 开始按钮点击
    /// </summary>
    public void BTStartOnClick()
    {
        uiManager.OpenUIAndCloseOtherByName("Data");
    }

    /// <summary>
    /// 设置按钮点击
    /// </summary>
    public void BTSettingOnClick()
    {
        uiManager.OpenUIAndCloseOtherByName("Setting");
    }

    /// <summary>
    /// 退出按钮点击
    /// </summary>
    public void BTExitOnClick()
    {
        GameUtil.ExitGame();
    }

}
