using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class LevelSkillsService 
{
    private readonly string mTableName;
    private readonly string mLeftTableName;

    public LevelSkillsService()
    {
        mTableName = "level_skills";
        mLeftTableName = "level_skills_details_" + GameCommonInfo.gameConfig.language;
    }

    /// <summary>
    /// 查询所有场景数据
    /// </summary>
    /// <returns></returns>
    public List<LevelSkillsBean> QueryAllData()
    {
        string[] leftTable = new string[] { mLeftTableName };
        string mainKey = "id";
        string[] leftKey = new string[] { "skills_id" };
        string[] colName = new string[] { "valid" };
        string[] operations = new string[] { "=" };
        string[] colValue = new string[] { 1 + "" };

        return SQliteHandle.LoadTableData<LevelSkillsBean>(ProjectConfigInfo.DATA_BASE_INFO_NAME, mTableName, leftTable, mainKey, leftKey, colName, operations, colValue);
    }
}