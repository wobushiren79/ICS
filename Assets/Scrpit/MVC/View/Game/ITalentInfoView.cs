using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public interface ITalentInfoView 
{
    /// <summary>
    /// 获取所有技能成功
    /// </summary>
    /// <param name="listData"></param>
    void GetTalentInfoDataSuccess(List<TalentInfoBean> listData);

    /// <summary>
    /// 获取所有技能失败
    /// </summary>
    void GetTalentInfoDataFail();
}