using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScenesModel : BaseMVCModel
{
    private LevelScenesService mLevelScenesService; 

    public override void InitData()
    {
        mLevelScenesService = new LevelScenesService();
    }

    /// <summary>
    /// 获取所有场景数据
    /// </summary>
    /// <returns></returns>
    public List<LevelScenesBean> GetAllLevelScenesData() {
       return mLevelScenesService.QueryAllData();
    }

    /// <summary>
    /// 根据场景等级获取场景数据
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public LevelScenesBean GetLevelScenesDataByLevel(int level)
    {
        List<LevelScenesBean> listData= mLevelScenesService.QueryDataByLevel(level);
        if (CheckUtil.ListIsNull(listData))
            return null;
        //如果查询有多个数据，说明数据有误，默认返回第一个
        return listData[0];
    }

    /// <summary>
    /// 获取指定等级范围的场景数据
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<LevelScenesBean> GetLevelScenesDataByLevel(int startLevel,int endLevel)
    {
        int count = endLevel - startLevel + 1;
        int[] levels = new int[count];
        for(int i = 0; i < count; i++)
        {
            levels[i] = startLevel+i;
        }
        return mLevelScenesService.QueryDataByLevel(levels);
    }

}
