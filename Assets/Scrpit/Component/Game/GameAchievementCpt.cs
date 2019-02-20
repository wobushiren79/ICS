using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class GameAchievementCpt : BaseMonoBehaviour,IGameDataCallBack
{
    public GameDataCpt gameDataCpt;

    public string apiGoodsLevel = "GOODS_LEVEL";
    public string apiNumberGoods = "NUMBER_LEVEL_";
    private void Start()
    {
        if (gameDataCpt != null)
        {
            gameDataCpt.AddObserver(this);
            GoodsLevelChange(gameDataCpt.userData.goodsLevel);
        }
    }

    public void GoodsLevelChange(int level)
    {
        SteamUserStatsHandle.UserStatsDataUpdate(apiGoodsLevel, level);
    }

    public void GoodsNumberChange(int level, int number, int totalNumber)
    {
        SteamUserStatsHandle.UserStatsDataUpdate(apiNumberGoods + level, totalNumber);
    }

    public void ObserbableUpdate(int type, params Object[] obj)
    {
    }

    public void ScoreChange(double score)
    {
    }

    public void ScoreLevelChange(int level)
    {
    }

    public void SpaceNumberChange(int level, int number,int totalNumber)
    {
    }
}