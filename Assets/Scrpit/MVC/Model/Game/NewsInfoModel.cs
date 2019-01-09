using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class NewsInfoModel : BaseMVCModel
{

    private NewsInfoService mNewInfoService;

    public override void InitData()
    {
        mNewInfoService = new NewsInfoService();
    }
    
    /// <summary>
    /// 获取所有新闻内容
    /// </summary>
    /// <returns></returns>
    public List<NewsInfoBean> GetAllNewsInfo()
    {
       return  mNewInfoService.QueryAllData();
    }

    /// <summary>
    /// 查询指定范围内的新闻内容
    /// </summary>
    /// <param name="startLevel"></param>
    /// <param name="endLevel"></param>
    /// <returns></returns>
    public List<NewsInfoBean> GetNewsInfoByLevel(int startLevel, int endLevel) {
        return mNewInfoService.QueryDataByLevel(startLevel, endLevel);
    }
}