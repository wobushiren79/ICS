using System.Collections;
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

    }
}
