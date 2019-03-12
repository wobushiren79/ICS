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

    public enum ItemStatus
    {
        Normal,//普通
        Invalid,//失效
        Hide//隐藏
    }
    public Text tvName;
    public Text tvNumber;
    public Text tvPrice;
    public Image ivIcon;
    public Image ivIconBorder;
    public Image ivContentBorder;
    public Button btSubmit;

    public GameToastCpt toastCpt;
    //相关数据
    public LevelScenesBean levelScenesBean;
    public GameDataCpt gameDataCpt;
    private UserItemLevelBean mUserLevelData;

    public Sprite thumbIcon;//缩略图
    public StoreItemType itemType;

    public Color noMoneyColor = new Color(1,0,0);
    public Color hasMoneyColor = new Color(0,1,0);
    public double goodsPirce = 0;
    private void Awake()
    {
        if (gameDataCpt != null)
            gameDataCpt.AddObserver(this);
    }
    private void Update()
    {
        if (goodsPirce > gameDataCpt.userData.userScore)
        {
            tvPrice.color = noMoneyColor;
        }
        else
        {
            tvPrice.color = hasMoneyColor;
        }
    }
    private void OnDestroy()
    {
        if (gameDataCpt != null)
            gameDataCpt.RemoveObserver(this);
    }

    public void SetData(StoreItemType itemType, Sprite icon, Sprite thumbIcon, LevelScenesBean itemData)
    {
        this.levelScenesBean = itemData;
        this.thumbIcon = thumbIcon;
        this.itemType = itemType;
        mUserLevelData = gameDataCpt.GetUserItemLevelDataByLevel(this.levelScenesBean.level);

        string nameStr = "---";
        int number = 0;
        switch (itemType)
        {
            case StoreItemType.Goods:
                nameStr = itemData.goods_name;
                if (mUserLevelData != null)
                    number = mUserLevelData.goodsNumber;
                goodsPirce = PriceConversion(StoreItemType.Goods, this.levelScenesBean.goods_sell_price, number);
                break;
            case StoreItemType.Space:
                nameStr = itemData.space_name;
                if (mUserLevelData != null)
                    number = mUserLevelData.spaceNumber;
                goodsPirce = PriceConversion(StoreItemType.Space, this.levelScenesBean.space_sell_price, number);
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
        SetPrice(goodsPirce);
        //设置按钮
        if (btSubmit != null)
        {
            btSubmit.onClick.RemoveAllListeners();
            btSubmit.onClick.AddListener(delegate ()
            {
                gameObject.transform.DOKill();
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                gameObject.transform.DOScale(new Vector3(0.8f, 0.8f), 0.2f).From();
                // gameObject.transform.DOShakeScale(1f, new Vector3(1.1f, 1.1f));
                //是否扣款成功
                bool isRemove = false;
                //是否有足够的地皮
                bool isSpace = true;
                switch (itemType)
                {
                    case StoreItemType.Goods:
                        isSpace = gameDataCpt.HasSpaceToAddGoodsByLevel(mUserLevelData, 1);
                        if (isSpace)
                            isRemove = gameDataCpt.RemoveScore(PriceConversion(StoreItemType.Goods, this.levelScenesBean.goods_sell_price, mUserLevelData.goodsNumber));
                        if (isRemove && isSpace)
                            gameDataCpt.AddLevelGoods(itemData.level, 1);
                        break;
                    case StoreItemType.Space:
                        int spaceNumber = 0;
                        if (mUserLevelData != null)
                            spaceNumber = mUserLevelData.spaceNumber;
                        isRemove = gameDataCpt.RemoveScore(PriceConversion(StoreItemType.Space, this.levelScenesBean.space_sell_price, spaceNumber));
                        if (isRemove)
                            gameDataCpt.AddLevelSpace(itemData.level, 1);
                        break;
                }
                if (!isRemove && toastCpt != null)
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
           

        //设置不同状态的按钮
        if (gameDataCpt.userData.scoreLevel >= levelScenesBean.level)
        {

        }
        else if (levelScenesBean.level - 1 == gameDataCpt.userData.scoreLevel)
        {
            if (ivIcon != null)
                ivIcon.color = Color.HSVToRGB(0, 0, 0);
            if (tvName != null)
                tvName.text = "????";
            if (btSubmit != null)
                btSubmit.onClick.RemoveAllListeners();
            if (ivIconBorder != null)
                ivIconBorder.color = new Color(0.5f, 0.5f, 0.5f);
            if (ivContentBorder != null)
                ivContentBorder.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SetStatus()
    {

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
        if (levelScenesBean.level - 1 == gameDataCpt.userData.scoreLevel)
            infoPopupView.gameObject.SetActive(false);
        int number = 0;
        string nameStr = "---";
        string introductionStr = "---";
        string otherStr = "";
        switch (itemType)
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

                    string itemTempGrow = GameCommonInfo.GetPriceStr(mUserLevelData.itemGrow * mUserLevelData.itemTimes,2);
                    string totalTempGrow = GameCommonInfo.GetPriceStr(mUserLevelData.itemGrow * number * mUserLevelData.itemTimes,2);
                    otherStr =
                        "➤ " + GameCommonInfo.GetTextById(41) + " " + nameStr + " " + GameCommonInfo.GetTextById(39) + GameCommonInfo.GetTextById(42) + itemTempGrow + GameCommonInfo.GetTextById(40) + "\n" +
                        "➤ " + number + " " + GameCommonInfo.GetTextById(43) + " " + nameStr + " " + GameCommonInfo.GetTextById(39) + GameCommonInfo.GetTextById(42) + totalTempGrow + GameCommonInfo.GetTextById(40);
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
                price = originalPrice * Math.Pow(50f, number);
                break;
        }
        return price;
    }

    #region 游戏数据回调
    public void GoodsNumberChange(int level, int number, int totalNumber)
    {
        NumberChange(StoreItemType.Goods);
    }

    public void SpaceNumberChange(int level, int number, int totalNumber)
    {
        NumberChange(StoreItemType.Space);
    }


    public void ScoreChange(double score)
    {

    }

    public void ObserbableUpdate(int type, params UnityEngine.Object[] obj)
    {
    }

    public void ScoreLevelChange(int level)
    {
       
    }

    public void GoodsLevelChange(int level)
    {
  
    }
    #endregion

    private void NumberChange(StoreItemType type)
    {
        if (mUserLevelData == null)
        {
            mUserLevelData = gameDataCpt.GetUserItemLevelDataByLevel(this.levelScenesBean.level);
            if (mUserLevelData == null)
            {
                return;
            }
        }
        int number = 0;
        double originalPrice = 0;
        switch (type)
        {
            case StoreItemType.Goods:
                number = mUserLevelData.goodsNumber;
                originalPrice = this.levelScenesBean.goods_sell_price;
                break;
            case StoreItemType.Space:
                number = mUserLevelData.spaceNumber;
                originalPrice = this.levelScenesBean.space_sell_price;
                break;
        }
        SetNumber(number);
        double pirce = PriceConversion(type, originalPrice, number);
        SetPrice(pirce);
        OpenPopup();
    }
}