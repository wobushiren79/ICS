using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class TalentInfoController : BaseMVCController<TalentInfoModel, ITalentInfoView>
{
    public TalentInfoController(BaseMonoBehaviour content, ITalentInfoView view) : base(content, view)
    {
    }

    public override void InitData()
    {
       
    }

    /// <summary>
    /// 获取所有天赋数据
    /// </summary>
    public void GetAllTalentInfo()
    {
        List<TalentInfoBean> listData = GetModel().GetAllLevelSkills();
        if (listData == null)
            GetView().GetTalentInfoDataFail();
        else
            GetView().GetTalentInfoDataSuccess(listData);
    }
}