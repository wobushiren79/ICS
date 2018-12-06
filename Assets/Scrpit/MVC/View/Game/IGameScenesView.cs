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
    void CreateLevelScenes(LevelScenesBean levelScenesData,UserItemLevelBean itemLevelData);
}
