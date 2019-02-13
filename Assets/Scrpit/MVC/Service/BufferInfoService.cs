using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BufferInfoService 
{
    private readonly string mTableName;
    private readonly string mLeftTableName;

    public BufferInfoService()
    {
        mTableName = "buffer_info";
        mLeftTableName = "buffer_info_details_" + GameCommonInfo.LANGUAGE;
    }

    /// <summary>
    /// 查询所有BUFFER数据
    /// </summary>
    /// <returns></returns>
    public List<BufferInfoBean> QueryAllData()
    {
        string[] leftTable = new string[] { mLeftTableName };
        string mainKey = "id";
        string[] leftKey = new string[] { "buffer_id" };
        string[] colName = new string[] { "valid" };
        string[] operations = new string[] { "=" };
        string[] colValue = new string[] { 1 + "" };

        return SQliteHandle.LoadTableData<BufferInfoBean>(ProjectConfigInfo.DATA_BASE_INFO_NAME, mTableName, leftTable, mainKey, leftKey, colName, operations, colValue);
    }

    /// <summary>
    /// 根据等级查询Buffer数据
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<BufferInfoBean> QueryDataByLevel(int[] levels)
    {
        string[] leftTable = new string[] { mLeftTableName };
        string mainKey = "id";
        string[] leftKey = new string[] { "level_id" };

        string[] colName = new string[] { "level" };
        string[] operations = new string[] { "IN" };
        string values = TypeConversionUtil.ArrayToStringBySplit(levels, ",");
        string[] colValue = new string[] { "(" + values + ")" };
        return SQliteHandle.LoadTableData<BufferInfoBean>(ProjectConfigInfo.DATA_BASE_INFO_NAME, mTableName, leftTable, mainKey, leftKey, colName, operations, colValue);
    }
}