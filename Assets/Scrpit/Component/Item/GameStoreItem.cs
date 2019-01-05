using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

public class GameStoreItem : PopupReplyView, IGameDataCallBack
{
    public enum StoreItemType
    {
        Goods,//商品
        Space,//地皮
    }
    public Text tvName;
    public Text tvNumber;
    public Text tvPrice;
    public Image ivIcon;
    public Button btSubmit;

    public GameToastCpt toastCpt;
    //相关数据
    public LevelScenesBean levelScenesBean;
    public GameDataCpt gameData;
    private UserItemLevelBean mUserLevelData;

    public Sprite thumbIcon;//缩略图
    public StoreItemType itemType;
    private void Awake()
    {
        if (gameData != null)
            gameData.AddObserver(this);
    }

    private void OnDestroy()
    {
        if (gameData != null)
            gameData.RemoveObserver(this);
    }

    public void SetData(StoreItemType itemType, Sprite icon, Sprite thumbIcon, LevelScenesBean itemData)
    {
        this.levelScenesBean = itemData;
        this.thumbIcon = thumbIcon;
        this.itemType = itemType;
        mUserLevelData = gameData.GetUserItemLevelDataByLevel(this.levelScenesBean.level);

        string nameStr = "---";
        int number = 0;
        double pirce = 0;
        switch (itemType)
        {
            case StoreItemType.Goods:
                nameStr = itemData.goods_name;
                if (mUserLevelData != null)
                    number = mUserLevelData.goodsNumber;
                pirce = PriceConversion(StoreItemType.Goods, this.levelScenesBean.goods_sell_price, number);
                break;
            case StoreItemType.Space:
                nameStr = itemData.space_name;
                if (mUserLevelData != null)
                    number = mUserLevelData.spaceNumber;
                pirce = PriceConversion(StoreItemType.Space, this.levelScenesBean.space_sell_price, number);
                break;
        }
        //设置名字
        if (tvName != null)
            tvName.text = nameStr;
        //设置图标
        if (ivIcon != null)
            ivIcon.sprite = icon;
        //设置数量
        SetNumber(number);
        //设置价格
        SetPrice(pirce);
        //设置按钮
        if (btSubmit != null)
            btSubmit.onClick.AddListener(delegate ()
            {
                gameObject.transform.DOKill();
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                gameObject.transform.DOShakeScale(1f, new Vector3(1.1f, 1.1f));
                //是否扣款成功
                bool isRemove = false;
                //是否有足够的地皮
                bool isSpace = true;
                switch (itemType)
                {
                    case StoreItemType.Goods:
                        if (mUserLevelData==null||mUserLevelData.goodsNumber >= mUserLevelData.spaceNumber * 25)
                            isSpace = false;
                        if (isSpace)
                            isRemove = gameData.RemoveScore(PriceConversion(StoreItemType.Goods, this.levelScenesBean.goods_sell_price, mUserLevelData.goodsNumber));
                        if (isRemove&&isSpace)
                            gameData.AddLevelGoods(itemData.level, 1);
                        break;
                    case StoreItemType.Space:
                        int spaceNumber = 0;
                        if (mUserLevelData != null)
                            spaceNumber = mUserLevelData.spaceNumber;
                        isRemove = gameData.RemoveScore(PriceConversion(StoreItemType.Space, this.levelScenesBean.space_sell_price, spaceNumber));
                        if (isRemove)
                            gameData.AddLevelSpace(itemData.level, 1);
                        break;
                }
                if (!isRemove&&toastCpt!=null)
                {
                    if (isSpace)
                    {
                        toastCpt.ToastHint(GameCommonInfo.GetTextById(44));
                    }
                    else
                    {
                        toastCpt.ToastHint(GameCommonInfo.GetTextById(45));
                    }
                }
            });
    }

    /// <summary>
    /// 设置数量
    /// </summary>
    /// <param name="number"></param>
    public void SetNumber(int number)
    {
        if (tvNumber != null)
            tvNumber.text = number + "";
    }

    /// <summary>
    /// 设置价格
    /// </summary>
    /// <param name="price"></param>
    public void SetPrice(double price)
    {
        string priceStr = "";
        UnitUtil.UnitEnum outUnit;
        UnitUtil.DoubleToStrUnit(price, out priceStr, out outUnit);
        if (tvPrice != null)
            tvPrice.text = priceStr + " " + GameCommonInfo.GetUnitStr(outUnit);
    }

    public override void OpenPopup()
    {
        if (levelScenesBean == null)
            return;
        int number = 0;
        string nameStr = "---";
        string introductionStr = "---";
        string otherStr = "";
        ; switch (itemType)
        {
            case StoreItemType.Goods:
                if (levelScenesBean != null)
                {
                    nameStr = levelScenesBean.goods_name;
                    introductionStr = levelScenesBean.goods_introduction;
                }
                if (mUserLevelData != null)
                {
                    number = mUserLevelData.goodsNumber;
                    otherStr =
                        "➤ " + GameCommonInfo.GetTextById(41) + " " + nameStr + " " + GameCommonInfo.GetTextById(39) + GameCommonInfo.GetTextById(42) + mUserLevelData.itemGrow + GameCommonInfo.GetTextById(40) + "\n" +
                        "➤ " + number + " " + GameCommonInfo.GetTextById(43) + " " + nameStr + " " + GameCommonInfo.GetTextById(39) + GameCommonInfo.GetTextById(42) + mUserLevelData.itemGrow * number + GameCommonInfo.GetTextById(40);
                }
                break;
            case StoreItemType.Space:
                if (levelScenesBean != null)
                {
                    nameStr = levelScenesBean.space_name;
                    introductionStr = levelScenesBean.space_introduction;
                }
                if (mUserLevelData != null)
                {
                    number = mUserLevelData.spaceNumber;
                }
                break;
        }
        infoPopupView.SetInfoData(thumbIcon, nameStr, "[" + number + "]", tvPrice.text + "", introductionStr, otherStr);
    }

    public override void ClosePopup()
    {

    }

    /// <summary>
    /// 价格换算
    /// </summary>
    /// <param name="itemType">类型</param>
    /// <param name="originalPrice">原来的价格</param>
    /// <param name="number">数量</param>
    /// <returns></returns>
    private double PriceConversion(StoreItemType itemType, double originalPrice, int number)
    {
        double price = 0;
        switch (itemType)
        {
            case StoreItemType.Goods:
                price = originalPrice * Math.Pow(1.15f, number);
                break;
            case StoreItemType.Space:
                price = originalPrice * Math.Pow(2.5f, number);
                break;
        }
        return price;
    }

    #region 游戏数据回调
    public void GoodsNumberChange(int level, int number)
    {
        if (mUserLevelData == null)
            return;
        SetNumber(mUserLevelData.goodsNumber);
        double pirce = PriceConversion(StoreItemType.Goods, this.levelScenesBean.goods_sell_price, mUserLevelData.goodsNumber);
        SetPrice(pirce);
        OpenPopup();
    }

    public void SpaceNumberChange(int level, int number)
    {
        if (mUserLevelData == null)
            return;
        SetNumber(mUserLevelData.spaceNumber);
        double pirce = PriceConversion(StoreItemType.Space, this.levelScenesBean.space_sell_price, mUserLevelData.spaceNumber);
        SetPrice(pirce);
    }

    public void ScoreChange(double score)
    {
    }

    public void ObserbableUpdate(int type, params UnityEngine.Object[] obj)
    {
    }
    #endregion
}