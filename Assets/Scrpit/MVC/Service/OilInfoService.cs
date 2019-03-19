using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class OilInfoService
{
    private readonly string mTableName;
    private readonly string mLeftTableName;

    public OilInfoService()
    {
        mTableName = "oil_info";
        mLeftTableName = "oil_info_details_" + GameCommonInfo.gameConfig.language;
    }

    /// <summary>
    /// 查询所有场景数据
    /// </summary>
    /// <returns></returns>
    public List<OilInfoBean> QueryAllData()
    {
        string[] leftTable = new string[] { mLeftTableName };
        string mainKey = "id";
        string[] leftKey = new string[] { "oil_id" };
        string[] colName = new string[] { "valid" };
        string[] operations = new string[] { "=" };
        string[] colValue = new string[] { 1 + "" };

        return SQliteHandle.LoadTableData<OilInfoBean>(ProjectConfigInfo.DATA_BASE_INFO_NAME, mTableName, leftTable, mainKey, leftKey, colName, operations, colValue);
    }
}