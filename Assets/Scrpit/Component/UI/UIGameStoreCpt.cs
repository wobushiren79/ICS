using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameStoreCpt : BaseUIComponent, IGameScenesView
{
    //分数
    public Text tvScore;

    //按钮-类型-商品
    public Button btTypeGoods;
    //按钮-类型-地皮
    public Button btTypeSpace;
    //列表
    public GameObject listContentObj;
    //列表item模型
    public GameObject contentGoodsItemModel;
    public GameObject contentSpaceItemModel;
    public List<Sprite> listGoodsIcon;
    public List<Sprite> listSpaceIcon;

    //返回按钮
    public Button btBack;
    //游戏数据
    public GameDataCpt gameDataCpt;

    //游戏场景数据控制
    private GameScenesController mGameScenesController;
    private List<LevelScenesBean> mListScenesData;

    public int selectType = 1;

    private void Awake()
    {
        mGameScenesController = new GameScenesController(this, this);
        mGameScenesController.GetAllGameScenesData();
    }

    private void Start()
    {
        if (btTypeGoods != null)
            btTypeGoods.onClick.AddListener(TypeGoodsSelect);
        if (btTypeSpace != null)
            btTypeSpace.onClick.AddListener(TypeSpaceSelect);
        if (btBack != null)
            btBack.onClick.AddListener(BTBack);
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

    /// <summary>
    /// 商品类型按钮点击
    /// </summary>
    public void TypeGoodsSelect()
    {
        if (mListScenesData == null)
            return;
        selectType = 1;
        CptUtil.RemoveChildsByActive(listContentObj.transform);
        foreach (LevelScenesBean itemData in mListScenesData)
        {
            GameObject itemObj = Instantiate(contentGoodsItemModel, contentGoodsItemModel.transform);
            itemObj.SetActive(true);
            itemObj.transform.parent = listContentObj.transform;
            //设置按钮
            Button itemBT=  itemObj.GetComponent<Button>();
            itemBT.onClick.AddListener(delegate() {
                gameDataCpt.AddLevelGoods(itemData.level,1);
            });
            //设置名字
            Text itemName = CptUtil.GetCptInChildrenByName<Text>(itemObj, "GoodsName");
            if (itemName != null)
                itemName.text = itemData.goods_name;
            //设置图标
            Image itemIcon = CptUtil.GetCptInChildrenByName<Image>(itemObj, "GoodsIcon");
            if (itemIcon != null)
                itemIcon.sprite = listGoodsIcon[itemData.level - 1];
        }
    }

    /// <summary>
    /// 地皮类型按钮点击
    /// </summary>
    public void TypeSpaceSelect()
    {
        if (mListScenesData == null)
            return;
        selectType = 2;
        CptUtil.RemoveChildsByActive(listContentObj.transform);

        foreach (LevelScenesBean itemData in mListScenesData)
        {
            GameObject itemObj = Instantiate(contentSpaceItemModel, contentSpaceItemModel.transform);
            itemObj.SetActive(true);
            itemObj.transform.parent = listContentObj.transform;
            //设置按钮
            Button itemBT = itemObj.GetComponent<Button>();
            itemBT.onClick.AddListener(delegate () {
                gameDataCpt.AddLevelSpace(itemData.level, 1);
            });
            //设置名字
            Text itemName = CptUtil.GetCptInChildrenByName<Text>(itemObj, "SpaceName");
            if (itemName != null)
                itemName.text = itemData.space_name;
            //设置图标
            Image itemIcon = CptUtil.GetCptInChildrenByName<Image>(itemObj, "SpaceIcon");
            if (itemIcon != null)
                itemIcon.sprite = listSpaceIcon[itemData.level - 1];
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

    public void GetAllScenesDataSuccess(List<LevelScenesBean> listScenesData)
    {
        this.mListScenesData = listScenesData;
        TypeGoodsSelect();
    }

    public void GetScenesDataSuccessByUserData(LevelScenesBean levelScenesData, UserItemLevelBean itemLevelData)
    {

    }

    #endregion
}
