using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class NewsInfoService
{
    private readonly string mTableName;
    private readonly string mLeftTableName;

    public NewsInfoService()
    {
        mTableName = "news_info";
        mLeftTableName = "news_info_details_" + GameCommonInfo.gameConfig.language;
    }

    /// <summary>
    /// 查询所有场景数据
    /// </summary>
    /// <returns></returns>
    public List<NewsInfoBean> QueryAllData()
    {
        string[] leftTable = new string[] { mLeftTableName };
        string mainKey = "id";
        string[] leftKey = new string[] { "news_id" };
        string[] colName = new string[] { "valid" };
        string[] operations = new string[] { "=" };
        string[] colValue = new string[] { 1 + "" };

        return SQliteHandle.LoadTableData<NewsInfoBean>(ProjectConfigInfo.DATA_BASE_INFO_NAME, mTableName, leftTable, mainKey, leftKey, colName, operations, colValue);
    }

    public List<NewsInfoBean> QueryDataByLevel(int startLevel, int endLevel)
    {
        string[] leftTable = new string[] { mLeftTableName };
        string mainKey = "id";
        string[] leftKey = new string[] { "news_id" };
        string[] colName = new string[] { "valid","level" ,"level"};
        string[] operations = new string[] { "=","<=",">=" };
        string[] colValue = new string[] { 1 + "", endLevel+"", startLevel+"" };

        return SQliteHandle.LoadTableData<NewsInfoBean>(ProjectConfigInfo.DATA_BASE_INFO_NAME, mTableName, leftTable, mainKey, leftKey, colName, operations, colValue);

    }
}