using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class GameSkillsController : BaseMVCController<GameSkillsModel, IGameSkillsView>
{

    public GameSkillsController(BaseMonoBehaviour content, IGameSkillsView view) : base(content, view)
    {

    }

    public override void InitData()
    {

    }

    /// <summary>
    /// 获取所有等级技能数据
    /// </summary>
    public void GetAllLevelSkill()
    {
        List<LevelSkillsBean> listData = GetModel().GetAllLevelSkills();
        if (listData == null)
            GetView().GetAllLevelSkillsDataFail();
        else
            GetView().GetAllLevelSkillsDataSuccess(listData);
    }
}