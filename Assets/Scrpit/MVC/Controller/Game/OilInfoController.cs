using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class OilInfoController : BaseMVCController<OilInfoModel,IOilInfoView>
{
    public OilInfoController(BaseMonoBehaviour content, IOilInfoView view) : base(content, view)
    {
    }

    public override void InitData()
    {
    }
    /// <summary>
    /// 获取所有等级技能数据
    /// </summary>
    public void GetAllOilInfo()
    {
        List<OilInfoBean> listData=GetModel().GetAllOilInfo();
        if (listData == null)
            GetView().GetAllOilDataFail();
        else
            GetView().GetAllOilDataSuccess(listData);
    }
}