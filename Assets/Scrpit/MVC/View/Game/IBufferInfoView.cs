using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public interface IBufferInfoView 
{
    /// <summary>
    /// 获取所有BUFF信息成功
    /// </summary>
    /// <param name="listData"></param>
    void GetBufferInfoSuccess(List<BufferInfoBean> listData);

    /// <summary>
    /// 获取所有BUFF信息失败
    /// </summary>
    void GetBufferInfoFail();

}  