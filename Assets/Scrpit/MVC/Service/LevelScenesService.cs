using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScenesService
{
    private readonly string mTableName;
    private readonly string mLeftTableName;

    public LevelScenesService()
    {
        mTableName = "level_scenes";
        mLeftTableName = "level_scenes_details_" + GameConfigInfo.LANGUAGE;
    }

    /// <summary>
    /// 查询所有场景数据
    /// </summary>
    /// <returns></returns>
    public List<LevelScenesBean> QueryAllData()
    {
        return SQliteHandle.LoadTableData<LevelScenesBean>(ProjectConfigInfo.DATA_BASE_INFO_NAME, mTableName,new string[] { mLeftTableName } ,"id",new string[] { "level_id"});
    }

    /// <summary>
    /// 根据场景等级查询场景数据
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<LevelScenesBean> QueryDataByLevel(int level)
    {
        string[] leftTable = new string[] { mLeftTableName };
        string mainKey = "id";
        string[] leftKey = new string[] { "level_id"};
        string[] colName = new string[] { "level" };
        string[] operations = new string[] { "=" };
        string[] colValue = new string[] { level + "" };

        return SQliteHandle.LoadTableData<LevelScenesBean>(ProjectConfigInfo.DATA_BASE_INFO_NAME, mTableName, leftTable, mainKey, leftKey,colName, operations, colValue);
    }

    /// <summary>
    /// 根据场景等级查询场景数据
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<LevelScenesBean> QueryDataByLevel(int[] levels)
    {
        string[] leftTable = new string[] { mLeftTableName };
        string mainKey = "id";
        string[] leftKey = new string[] { "level_id" };

        string[] colName = new string[] { "level" };
        string[] operations = new string[] { "IN" };
        string values = TypeConversionUtil.ArrayToStringBySplit(levels, ",");
        string[] colValue = new string[] { "(" + values + ")" };
        return SQliteHandle.LoadTableData<LevelScenesBean>(ProjectConfigInfo.DATA_BASE_INFO_NAME, mTableName, leftTable, mainKey, leftKey,colName, operations, colValue);
    }
}
