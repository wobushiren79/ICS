using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class UIGameOilCpt : BaseUIComponent,IGameDataCallBack
{
    //返回按钮
    public Button btBack;
    public Text tvBack;

    //标题
    public Text tvTitle;
    //分数
    public Text tvScore;

    public GameObject listOilContent;
    public GameObject itemOilModel;

    //图标数据
    public List<GameDataCpt.IconKV> listIconData;
    public GameDataCpt gameDataCpt;
    public InfoPopupView popupView;

    private void Start()
    {
        gameDataCpt.AddObserver(this);
        if (btBack != null)
            btBack.onClick.AddListener(BTBackOnClick);
        if (tvBack != null)
            tvBack.text = GameCommonInfo.GetTextById(36);
        if (tvTitle != null)
            tvTitle.text = GameCommonInfo.GetTextById(111);
        CreateOilList();
    }


    private void Update()
    {
        if (gameDataCpt == null || tvScore == null)
            return;
        tvScore.text = (int)gameDataCpt.userData.chiliOil + "";
    }

    public override void OpenUI()
    {
        base.OpenUI();
        if (popupView != null)
            popupView.gameObject.SetActive(false);
    }

    public void CreateOilList()
    {

        if (listOilContent == null)
            return;
        if (itemOilModel == null)
            return;
        CptUtil.RemoveChildsByActive(listOilContent.transform);
        List<OilInfoBean> listData = gameDataCpt.listOilData;
        for (int i = 0; i < listData.Count; i++)
        {
            OilInfoBean itemData = listData[i];
            CreateOilItem(itemData);
        }
    }

    public void BTBackOnClick()
    {
        uiManager.OpenUIAndCloseOtherByName("GameMenu");
    }


    public void CreateOilItem(OilInfoBean itemData)
    {
        if (itemData.unlock_level > gameDataCpt.userData.goodsLevel)
        {
            return;
        }
        Sprite iconSp=null;
        for(int i=0;i< listIconData.Count; i++)
        {
            GameDataCpt.IconKV itemIcon = listIconData[i];
            if (itemIcon.key.Equals(itemData.icon_key))
            {
                iconSp = itemIcon.value;
            }
        }

        GameObject objOil=  Instantiate(itemOilModel, itemOilModel.transform);
        objOil.SetActive(true);
        objOil.transform.SetParent(listOilContent.transform);
        objOil.transform.DOScale(new Vector3(0,0,0),1).From();
        GameOilItemCpt oilItem= objOil.GetComponent<GameOilItemCpt>();
        oilItem.SetData(itemData, iconSp);
    }

    #region 数据回调
    public void GoodsNumberChange(int level, int number, int totalNumber)
    {
    }

    public void SpaceNumberChange(int level, int number, int totalNumber)
    {
    }

    public void ScoreChange(double score)
    {
    }

    public void ScoreLevelChange(int level)
    {
    }

    public void GoodsLevelChange(int level)
    {
        CreateOilList();
    }

    public void ObserbableUpdate(int type, params Object[] obj)
    {

    }
    #endregion
}