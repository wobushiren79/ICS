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
        GetView().GetBufferInfoSuccess(listData);
    }

    /// <summary>
    /// 根据等级查询BUFFER信息
    /// </summary>
    /// <param name="levels"></param>
    public void GetBufferInfoByLevel(int[] levels)
    {
        List<BufferInfoBean> listData = GetModel().GetBufferInfoByLevel(levels);
        GetView().GetBufferInfoSuccess(listData);
    }
}