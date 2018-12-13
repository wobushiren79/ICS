using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScenesCpt : BaseMonoBehaviour, IGameScenesView,IGameDataCallBack
{

    //场景创建控制
    private GameScenesController mGameScenesController;

    //游戏数据管理
    public GameDataCpt gameData;

    public GameObject itemSpaceModel;
    public GameObject itemGoodsModel;

    //等级纹理
    public List<Texture> listLevelSpaceTexture;

    //场景间隙
    public int scenesInterval = 50;
    //当前标记最后一个item 位置
    private Dictionary<int, Vector3> mMarkLocation;

    private void Awake()
    {
        gameData.AddObserver(this);
        mMarkLocation = new Dictionary<int, Vector3>();
        mGameScenesController = new GameScenesController(this, this);
    }

    // Use this for initialization
    void Start()
    {
        if (gameData == null)
            return;
        mGameScenesController.GetGameScenesDataByUserData(gameData.userData);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 根据等级获取场景位置
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public Vector3 GetScenesPositionByLevel(int level)
    {
        return new Vector3(0, scenesInterval * (level - 1));
    }

    /// <summary>
    /// 根据等级获取场景边界
    /// </summary>
    /// <param name="level"></param>
    /// <param name="leftX"></param>
    /// <param name="rightX"></param>
    public void GetScenesBorderByLevel(int level, out float leftX, out float rightX)
    {
        UserItemLevelBean levelData = gameData.GetUserItemLevelDataByLevel(level);
        if (levelData == null)
        {
            leftX = 0;
            rightX = 0;
            return;
        }
        leftX = 0 - itemSpaceModel.transform.localScale.x / 2f;
        rightX = 0 + itemSpaceModel.transform.localScale.x / 2f + (levelData.spaceNumber - 1) * itemSpaceModel.transform.localScale.x;
    }

    #region 场景数据回调
    /// <summary>
    /// 创建场景
    /// </summary>
    /// <param name="levelScenesData"></param>
    /// <param name="itemLevelData"></param>
    public void GetScenesDataSuccessByUserData(LevelScenesBean levelScenesData, UserItemLevelBean itemLevelData)
    {
        GameObject levelObj = new GameObject("LevelScene_" + levelScenesData.level);
        levelObj.transform.parent = transform;
        if (itemSpaceModel == null)
            return;
        //获取当前Y轴位置
        float objPositionY = scenesInterval * (levelScenesData.level - 1);
        levelObj.transform.position = new Vector3(levelObj.transform.position.x,objPositionY);

        //创建地形
        for (int i = 0; i < itemLevelData.spaceNumber; i++)
        {
            //设置地形位置
            Vector3 itemSpacePosition = new Vector3(i * itemSpaceModel.transform.localScale.x, objPositionY);
            GameObject levelSpaceItem = Instantiate(itemSpaceModel, itemSpacePosition, itemSpaceModel.transform.rotation);
            levelSpaceItem.SetActive(true);
            levelSpaceItem.transform.parent = levelObj.transform;
            GameItemSpaceCpt spaceCpt = levelSpaceItem.GetComponent<GameItemSpaceCpt>();

            //设置地形数据
            if (spaceCpt != null
                && listLevelSpaceTexture != null
                && levelScenesData.level <= listLevelSpaceTexture.Count)
            {
                spaceCpt.SetLevelData(levelScenesData.level);
                spaceCpt.SetLevelTexture(listLevelSpaceTexture[levelScenesData.level - 1]);
            }
        }

        //创建单位
        float tempX = -itemSpaceModel.transform.localScale.x / 2f;
        float tempZ = -itemSpaceModel.transform.localScale.y / 2f;
        mMarkLocation.Add(levelScenesData.level,new Vector3(tempX, objPositionY, tempZ));
        for(int i=0;i< itemLevelData.goodsNumber;i++)
        {
            CreateGoodsItem(levelScenesData.level, levelObj);
        }
    }

    public void GetAllScenesDataSuccess(List<LevelScenesBean> listScenesData)
    {
    
    }
    #endregion

    #region 数据改变回调
    public void GoodsNumberChange(int level, int number)
    {
        Transform levelParentTf =CptUtil.GetCptInChildrenByName<Transform>(gameObject, "LevelScene_"+level);
        for(int i = 0; i < number; i++)
        {
            CreateGoodsItem(level, levelParentTf.gameObject);
        }
    }

    public void ObserbableUpdate(int type, params Object[] obj)
    {

    }
    #endregion

    #region 私有方法
    /// <summary>
    /// 创建一个item
    /// </summary>
    /// <param name="level"></param>
    /// <param name="parentObj"></param>
    private void CreateGoodsItem(int level,GameObject parentObj)
    {
        Vector3 markLoaction=  mMarkLocation[level];
        Vector3 itemGoodsPosition = new Vector3(markLoaction.x + 0.5f, markLoaction.y + 0.5f, markLoaction.z + 0.5f);
        GameObject levelGoodsItem = Instantiate(itemGoodsModel, itemGoodsPosition, itemGoodsModel.transform.rotation);
        levelGoodsItem.SetActive(true);
        levelGoodsItem.transform.parent = parentObj.transform;

        markLoaction.z++;
        if (markLoaction.z >= 5)
        {
            markLoaction.z = -5;
            markLoaction.x++;
        }
        mMarkLocation[level] = markLoaction;
    }

    public void SpaceNumberChange(int level, int number)
    {
        
    }
    #endregion
}
