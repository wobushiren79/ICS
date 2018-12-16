using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class UserDataController : BaseMVCController<UserDataModel, IUserDataView>
{
    public UserDataController(BaseMonoBehaviour content, IUserDataView view) : base(content, view)
    {

    }

    public override void InitData()
    {

    }

    /// <summary>
    /// 获取所有用户数据
    /// </summary>
    public void GetAllUserData()
    {
        List<UserDataBean> listData = GetModel().GetAllUserData();
        if (CheckUtil.ListIsNull(listData))
        {
            GetView().GetUserDataFail(UserDataModel.UserDataFailEnum.Fail);
        }
        else
        {
            foreach (UserDataBean itemData in listData)
            {
                GetView().GetUserDataSuccess(itemData);
            }
        }
    }

    /// <summary>
    /// 根据ID获取用户数据
    /// </summary>
    /// <param name="userId"></param>
    public void GetUserData(string userId)
    {
        //获取数据
        UserDataBean userData = GetModel().GetUserData(userId, GetView());
        //通知获取数据成功
        if (userData != null)
            GetView().GetUserDataSuccess(userData);
    }

    /// <summary>
    /// 创建用户数据
    /// </summary>
    public void CreateUserData(string userName)
    {
        //创建用户数据
        UserDataBean userData = GetModel().CreateUserData(userName);
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