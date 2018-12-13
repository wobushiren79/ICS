using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameScenesView
{
    /// <summary>
    /// 创建对应等级场景
    /// </summary>
    /// <param name="levelScenesData"></param>
    /// <param name="itemLevelData"></param>
    void GetScenesDataSuccessByUserData(LevelScenesBean levelScenesData,UserItemLevelBean itemLevelData);

    /// <summary>
    /// 获取所有场景数据成功
    /// </summary>
    /// <param name="listScenesData"></param>
    void GetAllScenesDataSuccess(List<LevelScenesBean> listScenesData);
}
