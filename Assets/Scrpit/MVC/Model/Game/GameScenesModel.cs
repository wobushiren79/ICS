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
        if (listData == null || listData.Count == 0)
            return null;
        //如果查询有多个数据，说明数据有误，默认返回第一个
        return listData[0];
    }

}
