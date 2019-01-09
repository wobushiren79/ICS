using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIGameStoreCpt : BaseUIComponent,IGameDataCallBack
{
    //标题
    public Text tvTitle;
    //分数
    public Text tvScore;

    //按钮-类型-商品
    public Button btTypeGoods;
    public Text tvTypeGoods;
    //按钮-类型-地皮
    public Button btTypeSpace;
    public Text tvTypeSpace;
    //列表
    public GameObject listContentObj;
    //列表item模型
    public GameObject contentGoodsItemModel;
    public GameObject contentSpaceItemModel;
    public List<Sprite> listGoodsIcon;
    public List<Sprite> listSpaceIcon;

    //返回按钮
    public Button btBack;
    public Text tvBack;
    //游戏数据
    public GameDataCpt gameDataCpt;

    public int selectType = 1;

    private void Awake()
    {
    }

    private void Start()
    {
        gameDataCpt.AddObserver(this);

        if (btTypeGoods != null)
            btTypeGoods.onClick.AddListener(TypeGoodsSelect);
        if (btTypeSpace != null)
            btTypeSpace.onClick.AddListener(TypeSpaceSelect);
        if (btBack != null)
            btBack.onClick.AddListener(BTBack);
        if (tvTitle != null)
            tvTitle.text = GameCommonInfo.GetTextById(32);
        if (tvBack != null)
            tvBack.text = GameCommonInfo.GetTextById(36);
        if (tvTypeGoods != null)
            tvTypeGoods.text = GameCommonInfo.GetTextById(37);
        if (tvTypeSpace != null)
            tvTypeSpace.text = GameCommonInfo.GetTextById(38);

        TypeGoodsSelect(); 
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
            tvScore.text = outNumberStr + " " + GameCommonInfo.GetUnitStr(outUnit);
        }
    }

    /// <summary>
    /// 商品类型按钮点击
    /// </summary>
    public void TypeGoodsSelect()
    {
        List<LevelScenesBean> listScenesData = gameDataCpt.GetScenesListByLevel(gameDataCpt.userData.scoreLevel + 1);
        if (listScenesData == null)
            return;
        selectType = 1;
        CptUtil.RemoveChildsByActive(listContentObj.transform);
        for (int i = 0; i < listScenesData.Count; i++)
        {
            LevelScenesBean itemData = listScenesData[i];
            GameObject itemObj = Instantiate(contentGoodsItemModel, contentGoodsItemModel.transform);
            itemObj.SetActive(true);
            itemObj.transform.SetParent(listContentObj.transform);
            //动画
            itemObj.transform.localScale = new Vector3(0, 0, 1);
            itemObj
                .transform
                .DOScale(new Vector3(1, 1), 0.5f)
                .SetEase(Ease.OutBack)
                .SetDelay(i * 0.05f);
            //设置数据
            GameStoreItem itemCpt = itemObj.GetComponent<GameStoreItem>();
            itemCpt.SetData(GameStoreItem.StoreItemType.Goods, listGoodsIcon[itemData.level - 1], listGoodsIcon[itemData.level - 1], itemData);
        }
    }

    /// <summary>
    /// 地皮类型按钮点击
    /// </summary>
    public void TypeSpaceSelect()
    {
        List<LevelScenesBean> listScenesData=gameDataCpt.GetScenesListByLevel(gameDataCpt.userData.scoreLevel+1);
        if (listScenesData == null)
            return;
        selectType = 2;
        CptUtil.RemoveChildsByActive(listContentObj.transform);
        for (int i = 0; i < listScenesData.Count; i++)
        {
            LevelScenesBean itemData = listScenesData[i];
            GameObject itemObj = Instantiate(contentSpaceItemModel, contentSpaceItemModel.transform);
            itemObj.SetActive(true);
            itemObj.transform.parent = listContentObj.transform;
            //动画
            itemObj.transform.localScale = new Vector3(0, 0, 1);
            itemObj
                .transform
                .DOScale(new Vector3(1, 1), 0.5f)
                .SetEase(Ease.OutBack) 
                .SetDelay(i * 0.05f);
            //设置数据
            GameStoreItem itemCpt = itemObj.GetComponent<GameStoreItem>();
            itemCpt.SetData(GameStoreItem.StoreItemType.Space, listSpaceIcon[itemData.level - 1], listSpaceIcon[itemData.level - 1], itemData);
        }
    }

    /// <summary>
    /// 返回按钮点击
    /// </summary>
    public void BTBack()
    {
        uiManager.OpenUIAndCloseOtherByName("GameMenu");
    }


    #region  -------------------------场景数据回调-------------------------------

    public void GoodsNumberChange(int level, int number)
    {
    }

    public void SpaceNumberChange(int level, int number)
    {
    }

    public void ScoreChange(double score)
    {
    }
    public void ScoreLevelChange(int level)
    {
        switch (selectType)
        {
            case 1:
                TypeGoodsSelect();
                break;
            case 2:
                TypeSpaceSelect();
                break;
        }
    }

    public void GoodsLevelChange(int level)
    {

    }

    public void ObserbableUpdate(int type, params Object[] obj)
    {
    }
    #endregion
}
