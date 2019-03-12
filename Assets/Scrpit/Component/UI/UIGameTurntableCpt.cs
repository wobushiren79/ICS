using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
public class UIGameTurntableCpt : BaseUIComponent
{
    //返回按钮
    public Button btBack;
    public Text tvBack;

    public GameDataCpt gameDataCpt;
    private void Start()
    {
        if (btBack != null)
            btBack.onClick.AddListener(BTBackOnClick);
        if (tvBack != null)
            tvBack.text = GameCommonInfo.GetTextById(36);
    }


    /// <summary>
    /// 返回按钮点击
    /// </summary>
    public void BTBackOnClick()
    {
        uiManager.OpenUIAndCloseOtherByName("GameMenu");
    }
}