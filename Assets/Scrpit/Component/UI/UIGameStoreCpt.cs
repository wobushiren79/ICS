using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameStoreCpt : BaseUIComponent
{
    //分数
    public Text tvScore;

    //按钮-类型-商品
    public Button btTypeGoods;
    //按钮-类型-地皮
    public Button btTypeSpace;


    //返回按钮
    public Button btBack;
    //游戏数据
    public GameDataCpt gameDataCpt;

    public int selectType=1;
    private void Start()
    {
        if (btTypeGoods != null)
            btTypeGoods.onClick.AddListener(BTTypeGoodsSelect);
        if (btTypeSpace != null)
            btTypeSpace.onClick.AddListener(BTTypeSpaceSelect);
        if (btBack != null)
            btBack.onClick.AddListener(BTBack);
    }

    /// <summary>
    /// 商品类型按钮点击
    /// </summary>
    public void BTTypeGoodsSelect()
    {
        selectType = 1;
    }

    /// <summary>
    /// 地皮类型按钮点击
    /// </summary>
    public void BTTypeSpaceSelect()
    {
        selectType = 2;
    }

    /// <summary>
    /// 返回按钮点击
    /// </summary>
    public void BTBack()
    {
        uiManager.OpenUIAndCloseOtherByName("GameMenu");
    }

    /// <summary>
    /// 数据更新
    /// </summary>
    private void Update()
    {
        if (gameDataCpt == null)
            return;
        if (tvScore != null)
        {
            string outNumberStr;
            UnitUtil.UnitEnum outUnit;
            UnitUtil.DoubleToStrUnit(gameDataCpt.userData.userScore, out outNumberStr, out outUnit);
            tvScore.text = outNumberStr + " " + (int)outUnit;
        }
    }
}
