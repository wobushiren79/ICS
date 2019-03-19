using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class OilInfoModel : BaseMVCModel
{

    private OilInfoService mOilInfoService;

    public override void InitData()
    {
        mOilInfoService = new OilInfoService();
    }
    /// <summary>
    /// 获取辣椒油技能信息
    /// </summary>
    /// <returns></returns>
    public List<OilInfoBean> GetAllOilInfo()
    {
        return mOilInfoService.QueryAllData();
    }
}