using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class TalentInfoService 
{
    private readonly string mTableName;
    private readonly string mLeftTableName;

    public TalentInfoService()
    {
        mTableName = "talent_info";
        mLeftTableName = "talent_info_details_" + GameCommonInfo.gameConfig.language;
    }

    /// <summary>
    /// 查询所有场景数据
    /// </summary>
    /// <returns></returns>
    public List<TalentInfoBean> QueryAllData()
    {
        string[] leftTable = new string[] { mLeftTableName };
        string mainKey = "id";
        string[] leftKey = new string[] { "talent_id" };
        string[] colName = new string[] { "valid" };
        string[] operations = new string[] { "=" };
        string[] colValue = new string[] { 1 + "" };

        return SQliteHandle.LoadTableData<TalentInfoBean>(ProjectConfigInfo.DATA_BASE_INFO_NAME, mTableName, leftTable, mainKey, leftKey, colName, operations, colValue);
    }
}