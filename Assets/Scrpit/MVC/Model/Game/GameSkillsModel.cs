using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class GameSkillsModel : BaseMVCModel
{
    private LevelSkillsService mLevelSkillsService;

    public override void InitData()
    {
        mLevelSkillsService = new LevelSkillsService();
    }

    /// <summary>
    /// 获取所有技能信息
    /// </summary>
    /// <returns></returns>
    public List<LevelSkillsBean> GetAllLevelSkills()
    {
       return mLevelSkillsService.QueryAllData();
    }
}