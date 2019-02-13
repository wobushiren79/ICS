using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BufferInfoModel : BaseMVCModel
{
    private BufferInfoService mBufferInfoService;

    public override void InitData()
    {
        mBufferInfoService = new BufferInfoService();
    }

    /// <summary>
    /// 获取所有BUFFER信息
    /// </summary>
    /// <returns></returns>
    public List<BufferInfoBean> GetAllBufferInfo()
    {
        return mBufferInfoService.QueryAllData();
    }

    /// <summary>
    /// 根据等级查询BUFFER信息
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<BufferInfoBean> GetBufferInfoByLevel(int[] level)
    {
        return mBufferInfoService.QueryDataByLevel(level);
    }
}