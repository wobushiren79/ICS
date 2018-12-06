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
    public void GetScenesBorderByLevel(int level,out float leftX,out float rightX)
    {
       UserItemLevelBean levelData=  gameData.GetUserItemLevelDataByLevel(level);
        if (levelData == null)
        {
            leftX = 0;
            rightX = 0;
            return;
        }
        leftX = 0 - itemSpaceObj.transform.localScale.x/2f;
        rightX = 0 + itemSpaceObj.transform.localScale.x / 2f + (levelData.spaceNumber-1) * itemSpaceObj.transform.localScale.x;
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
    
        for (int i = 0; i < itemLevelData.spaceNumber; i++)
        {
            Vector3 itemSpacePosition = new Vector3(i * itemSpaceObj.transform.localScale.x, scenesInterval * (levelScenesData.level - 1));
            GameObject levelSpaceItem = Instantiate(itemSpaceObj, itemSpacePosition, itemSpaceObj.transform.rotation);
            levelSpaceItem.SetActive(true);
            levelSpaceItem.transform.parent = levelObj.transform;
        }

    }
}
