using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GameOilItemCpt : PopupReplyView
{
    public Text tvPrice;
    public GameObject objPrice;

    public Image ivIcon;
    public Image ivUp;
    public Button btOil;

    public OilInfoBean oilInfoBean;

    public GameDataCpt gameDataCpt;
    public GameToastCpt gameToastCpt;
    public GameBufferListCpt gameBufferListCpt;

    public Color colorHasPrice = new Color(0, 1, 0);
    public Color colorNoPrice = new Color(1, 0, 0);

    private void Update()
    {
        if (gameDataCpt == null || oilInfoBean == null)
            return;
        if (gameDataCpt.userData.chiliOil < oilInfoBean.price)
        {
            tvPrice.color = colorNoPrice;
        }
        else
        {
            tvPrice.color = colorHasPrice;
        }
    }

    public void SetData(OilInfoBean data, Sprite iconSp)
    {
        oilInfoBean = data;
        if (oilInfoBean == null)
            return;
        tvPrice.text = oilInfoBean.price + "";
        ivIcon.sprite = iconSp;
        btOil.onClick.RemoveAllListeners();
        btOil.onClick.AddListener(BTOilOnClick);
    }

    public void BTOilOnClick()
    {
        if (oilInfoBean == null)
            return;
        if (gameDataCpt.userData.chiliOil < oilInfoBean.price)
        {
            if (gameToastCpt != null)
                gameToastCpt.ToastHint(GameCommonInfo.GetTextById(114));
            return;
        }
        if (oilInfoBean.id >= 1 && oilInfoBean.id <= 15)
        {
            UserItemLevelBean userItemLevel= gameDataCpt.GetUserItemLevelDataByLevel(oilInfoBean.unlock_level);
            if(userItemLevel==null|| userItemLevel.goodsNumber == 0)
            {
                LevelScenesBean levelScene= gameDataCpt.GetScenesByLevel(oilInfoBean.unlock_level);
                if (gameToastCpt != null)
                    gameToastCpt.ToastHint( GameCommonInfo.GetTextById(51)+ levelScene.goods_name);
                return;
            }
            if (gameBufferListCpt != null)
            {
                BufferInfoBean bufferInfoBean = new BufferInfoBean();
                bufferInfoBean.time = 600;
                bufferInfoBean.add_grow = 1;
                bufferInfoBean.name = oilInfoBean.name;
                bufferInfoBean.content = oilInfoBean.content;
                bufferInfoBean.icon_key = oilInfoBean.icon_key;
                bufferInfoBean.level = oilInfoBean.unlock_level;
                gameBufferListCpt.AddBuffer(bufferInfoBean);
            }
            
        }
        else if (oilInfoBean.id == 101)
        {
            if (gameDataCpt.userData.rebirthData == null)
                gameDataCpt.userData.rebirthData = new RebirthBean();
            gameDataCpt.userData.rebirthData.rebirthChili += 2;
        }
        gameDataCpt.userData.chiliOil -= (float)oilInfoBean.price;
        if (gameToastCpt != null)
            gameToastCpt.CreateToast(ivIcon.sprite, GameCommonInfo.GetTextById(113), oilInfoBean.other, 5);
    }

    private void ItemUIDeal()
    {
        if (oilInfoBean == null)
            return;
        if (oilInfoBean.id >= 1 && oilInfoBean.id <= 15)
        {
            ivUp.gameObject.SetActive(true);
        }
        else
        {
            ivUp.gameObject.SetActive(false);
        }
    }

    public override void ClosePopup()
    {
        ivIcon.transform.DOKill();
        ivIcon.transform.localScale = new Vector3(1, 1, 1);
    }

    public override void OpenPopup()
    {
        ivIcon.transform.DOShakeScale(3, 0.5f, 5, 45);
        string titleStr = oilInfoBean.name;
        string priceStr = oilInfoBean.price + "";
        string contentStr = oilInfoBean.content + "";
        string otherStr = oilInfoBean.other;
        infoPopupView.SetInfoData(ivIcon.sprite, titleStr, " ", priceStr, contentStr, otherStr, 1);
    }
}
