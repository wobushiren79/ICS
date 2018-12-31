using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.EventSystems;

public class PopupReplyGoodsView : PopupReplyView
{
    public LevelScenesBean levelScenesBean;

    public void SetData(LevelScenesBean itemData)
    {
        this.levelScenesBean = itemData;
    }

    public override void OpenPopup()
    {
        if (levelScenesBean == null)
            return;
        infoPopupView.SetInfoData(null, levelScenesBean.goods_name,"[test]", levelScenesBean.goods_sell_price+"","test","");
    }

    public override void ClosePopup()
    {
      
    }
}