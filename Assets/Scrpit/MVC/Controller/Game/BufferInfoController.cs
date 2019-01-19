using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BufferInfoController : BaseMVCController<BufferInfoModel, IBufferInfoView>
{
    public BufferInfoController(BaseMonoBehaviour content, IBufferInfoView view) : base(content, view)
    {

    }

    public override void InitData()
    {
   
    }

    /// <summary>
    /// 获取所有BUFF信息
    /// </summary>
    public void GetAllBufferInfo()
    {
        List<BufferInfoBean> listData= GetModel().GetAllBufferInfo();
        GetView().GetAllBufferInfoSuccess(listData);
    }
}