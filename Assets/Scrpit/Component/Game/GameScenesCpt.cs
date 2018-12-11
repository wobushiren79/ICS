using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScenesCpt : BaseMonoBehaviour, IGameScenesView
{

    //场景创建控制
    private GameScenesController mGameScenesController;

    //游戏数据管理
    public GameDataCpt gameData;

    public GameObject itemSpaceObj;
    public GameObject itemGoodsObj;

    //等级纹理
    public List<Texture> listLevelSpaceTexture;

    //场景间隙
    public int scenesInterval = 50;

    private void Awake()
    {
        mGameScenesController = new GameScenesController(this, this);
    }

    // Use this for initialization
    void Start()
    {
        if (gameData == null)
            return;
        mGameScenesController.CreateGameScenesByUserData(gameData.userData);
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
        leftX = 0 - itemSpaceObj.transform.localScale.x / 2f;
        rightX = 0 + itemSpaceObj.transform.localScale.x / 2f + (levelData.spaceNumber - 1) * itemSpaceObj.transform.localScale.x;
    }


    /// <summary>
    /// 创建场景
    /// </summary>
    /// <param name="levelScenesData"></param>
    /// <param name="itemLevelData"></param>
    public void CreateLevelScenes(LevelScenesBean levelScenesData, UserItemLevelBean itemLevelData)
    {
        GameObject levelObj = new GameObject("LevelScene_" + levelScenesData.level);
        levelObj.transform.parent = transform;
        if (itemSpaceObj == null)
            return;
        //获取当前Y轴位置
        float objPositionY = scenesInterval * (levelScenesData.level - 1);
        levelObj.transform.position = new Vector3(levelObj.transform.position.x,objPositionY);

        //创建地形
        for (int i = 0; i < itemLevelData.spaceNumber; i++)
        {
            //设置地形位置
            Vector3 itemSpacePosition = new Vector3(i * itemSpaceObj.transform.localScale.x, objPositionY);
            GameObject levelSpaceItem = Instantiate(itemSpaceObj, itemSpacePosition, itemSpaceObj.transform.rotation);
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
        float tempX = -itemSpaceObj.transform.localScale.x/2f;
        float tempZ = -itemSpaceObj.transform.localScale.y/2f;
        for(int i = 0; i < itemLevelData.goodsNumber; i++)
        {

            Vector3 itemGoodsPosition = new Vector3(tempX+0.5f, objPositionY+0.5f, tempZ+0.5f);
            GameObject levelGoodsItem = Instantiate(itemGoodsObj, itemGoodsPosition, itemGoodsObj.transform.rotation);
            levelGoodsItem.SetActive(true);
            levelGoodsItem.transform.parent = levelObj.transform;

            tempZ++;
            if (tempZ >= 5)
            {
                tempZ = -5;
                tempX++;
            }
        }
    }
}
