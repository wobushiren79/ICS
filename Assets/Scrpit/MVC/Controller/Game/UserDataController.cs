﻿using UnityEngine;
using UnityEditor;

public class UserDataController : BaseMVCController<UserDataModel, IUserDataView>
{
    public UserDataController(BaseMonoBehaviour content, IUserDataView view) : base(content, view)
    {

    }

    public override void InitData()
    {

    }

    /// <summary>
    /// 创建用户数据
    /// </summary>
    public void CreateUserData()
    {
        //创建用户数据
        UserDataBean userData = GetModel().CreateUserData();
        //通知创建成功
        if (userData != null)
            GetView().CreateUserDataSuccess(userData);
    }

    /// <summary>
    /// 保存用户数据
    /// </summary>
    /// <param name="userData"></param>
    public void SaveUserData(UserDataBean userData)
    {
        //保存用户数据
        GetModel().SaveUserData(userData, GetView());
        //通知保存成功
        if (userData != null)
            GetView().SaveUserDataSuccess(userData);
    }

    /// <summary>
    /// 删除用户数据
    /// </summary>
    /// <param name="userId"></param>
    public void DeleteUserData(string userId)
    {
        //删除用户数据
        UserDataBean userData = GetModel().DeleteUserData(userId, GetView());
        //通知保存成功
        if (userData != null)
            GetView().DeleteUserDataSuccess(userData);
    }
}