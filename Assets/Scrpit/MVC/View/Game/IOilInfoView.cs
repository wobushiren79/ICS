using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public interface IOilInfoView 
{
    /// <summary>
    /// 获取所有技能成功
    /// </summary>
    /// <param name="listData"></param>
    void GetAllOilDataSuccess(List<OilInfoBean> listData);

    /// <summary>
    /// 获取所有技能失败
    /// </summary>
    void GetAllOilDataFail();
}