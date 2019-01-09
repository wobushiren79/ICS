using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class NewsInfoController : BaseMVCController<NewsInfoModel, INewsInfoView>
{
    public NewsInfoController(BaseMonoBehaviour content, INewsInfoView view) : base(content, view)
    {

    }

    public override void InitData()
    {
      
    }

    public void GetNewsInfoByLevel(int startLevel,int endLevel)
    {
        List<NewsInfoBean> listData= GetModel().GetNewsInfoByLevel(startLevel, endLevel);
        if (listData == null)
        {
            GetView().GetNewsInfoDataFail();
            return;
        }
        GetView().GetNewsInfoDataSuccess(listData);
    }
}