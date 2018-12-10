using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;

public class GameDataCpt : BaseMonoBehaviour, IUserDataView
{

    //用户数据
    public UserDataBean userData;

    //用户数据管理
    private UserDataController mUserDataController;

    private void Awake()
    {
        mUserDataController = new UserDataController(this,this);
        mUserDataController.GetUserData("UserId_260e8c7c6a824cdcb09221e31253d01f");
    }

    private void FixedUpdate()
    {
        float updateNumber =  1/Time.fixedDeltaTime;
        if (updateNumber <= 0)
            return;
        double tempGrow = userData.userGrow / updateNumber;
        userData.userScore += tempGrow;
    }

    /// <summary>
    /// 保存当前数据
    /// </summary>
    /// <returns></returns>
    public void SaveUserData()
    {
        mUserDataController.SaveUserData(userData);
    }

    /// <summary>
    /// 获取用户名称
    /// </summary>
    /// <returns></returns>
    public string GetUserName()
    {
        if (CheckUtil.StringIsNull(userData.userName))
            return "";
        else
            return userData.userName;
    }

    /// <summary>
    /// 根据等级获取等级数据
    /// </summary>
    /// <param name="level"></param>
    public UserItemLevelBean GetUserItemLevelDataByLevel(int level)
    {
        if (userData == null||CheckUtil.ListIsNull(userData.itemLevelList))
            return null;
        List<UserItemLevelBean> listLevelData = userData.itemLevelList;
        foreach (UserItemLevelBean itemLevel in listLevelData)
        {
            if (itemLevel.level.Equals(level))
            {
                return itemLevel;
            }
        }
        return null;
    }


    #region  -----------------------------IUserDataView 回调--------------------------------------------
    public void GetUserDataSuccess(UserDataBean userData)
    {
        this.userData = userData;
    }

    public void GetUserDataFail(UserDataModel.UserDataFailEnum failEnum)
    {
    }

    public void CreateUserDataFail(UserDataModel.UserDataFailEnum failEnum)
    {

    }

    public void CreateUserDataSuccess(UserDataBean userData)
    {

    }

    public void DeleteUserDataFail(UserDataModel.UserDataFailEnum failEnum)
    {

    }

    public void DeleteUserDataSuccess(UserDataBean userData)
    {

    }

    public void SaveUserDataFail(UserDataModel.UserDataFailEnum failEnum)
    {

    }

    public void SaveUserDataSuccess(UserDataBean userData)
    {

    }
    #endregion



}