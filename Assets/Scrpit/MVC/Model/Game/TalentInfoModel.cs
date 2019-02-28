using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class TalentInfoModel :BaseMVCModel
{
    private TalentInfoService mTalentInfoService;

    public override void InitData()
    {
        mTalentInfoService = new TalentInfoService();
    }

    /// <summary>
    /// 获取所有天赋信息
    /// </summary>
    /// <returns></returns>
    public List<TalentInfoBean> GetAllLevelSkills()
    {
        return mTalentInfoService.QueryAllData();
    }
}