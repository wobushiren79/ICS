﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScenesController : BaseMVCController<GameScenesModel, IGameScenesView>
{

    public GameScenesController(BaseMonoBehaviour content, IGameScenesView view) : base(content, view)
    {

    }

    public override void InitData()
    {
        
    }


    /// <summary>
    /// 根据用户数据创建等级场景
    /// </summary>
    /// <param name="userData"></param>
    public void CreateGameScenesByUserData(UserDataBean userData)
    {
        if (userData == null)
            return;
        if (CheckUtil.StringIsNull(userData.userId))
            return;
        //获取场景数据
        List<LevelScenesBean> listLevelScenesData=  GetModel().GetLevelScenesDataByLevel(1,userData.userLevel);
        foreach (LevelScenesBean itemScenes in listLevelScenesData)
        {
            foreach (UserItemLevelBean itemLevelData in  userData.itemLevelList)
            {
                if (itemLevelData.level.Equals(itemScenes.level))
                {
                    GetView().CreateLevelScenes(itemScenes, itemLevelData);
                    break;
                }
            }
        }
    }


    
}
