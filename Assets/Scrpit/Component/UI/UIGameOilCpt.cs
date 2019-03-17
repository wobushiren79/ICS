using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UIGameOilCpt : BaseUIComponent
{
    //返回按钮
    public Button btBack;
    public Text tvBack;

    //标题
    public Text tvTitle;
    //分数
    public Text tvScore;

    private void Start()
    {
        if (btBack != null)
            btBack.onClick.AddListener(BTBackOnClick);
        if (tvBack != null)
            tvBack.text = GameCommonInfo.GetTextById(36);
        if (tvTitle != null)
            tvTitle.text = GameCommonInfo.GetTextById(111);
    }

    public void BTBackOnClick()
    {
        uiManager.OpenUIAndCloseOtherByName("GameMenu");
    }

}