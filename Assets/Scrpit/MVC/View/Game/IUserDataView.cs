using UnityEngine;
using UnityEditor;

public interface IUserDataView 
{
    /// <summary>
    /// 获取用户数据成功
    /// </summary>
    /// <param name="userData"></param>
    void GetUserDataSuccess(UserDataBean userData);

    /// <summary>
    /// 获取用户数据失败
    /// </summary>
    /// <param name="failEnum"></param>
    void GetUserDataFail(UserDataModel.UserDataFailEnum failEnum);

    /// <summary>
    /// 创建用户数据成功
    /// </summary>
    /// <param name="userData"></param>
    void CreateUserDataSuccess(UserDataBean userData);

    /// <summary>
    /// 创建用户数据失败
    /// </summary>
    /// <param name="msg"></param>
    void CreateUserDataFail(UserDataModel.UserDataFailEnum failEnum);

    /// <summary>
    /// 保存用户数据成功
    /// </summary>
    /// <param name="userData"></param>
    void SaveUserDataSuccess(UserDataBean userData);

    /// <summary>
    /// 保存用户数据失败
    /// </summary>
    /// <param name="msg"></param>
    void SaveUserDataFail(UserDataModel.UserDataFailEnum failEnum);

    /// <summary>
    /// 删除用户数据成功
    /// </summary>
    /// <param name="userData"></param>
    void DeleteUserDataSuccess(UserDataBean userData);

    /// <summary>
    /// 删除用户数据失败
    /// </summary>
    /// <param name="failEnum"></param>
    void DeleteUserDataFail(UserDataModel.UserDataFailEnum failEnum);
}