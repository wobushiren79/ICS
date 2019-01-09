using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public interface INewsInfoView 
{
    /// <summary>
    /// 获取新闻数据成功
    /// </summary>
    /// <param name="listData"></param>
    void GetNewsInfoDataSuccess(List<NewsInfoBean> listData);

    /// <summary>
    /// 获取新闻数据失败
    /// </summary>
    void GetNewsInfoDataFail();
}