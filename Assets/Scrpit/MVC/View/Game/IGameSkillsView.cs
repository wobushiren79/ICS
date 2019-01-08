using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public interface IGameSkillsView 
{
    /// <summary>
    /// 获取所有技能成功
    /// </summary>
    /// <param name="listData"></param>
    void GetAllLevelSkillsDataSuccess(List<LevelSkillsBean> listData);

    /// <summary>
    /// 获取所有技能失败
    /// </summary>
    void GetAllLevelSkillsDataFail();
}