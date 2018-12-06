﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class UserDataModel : BaseMVCModel
{
    public enum UserDataFailEnum
    {
        Fail,//失败
        NoData,//没有数据
        NoUserId,//没用用户ID
    }

    private UserDataService mUserDataService; 

    public override void InitData()
    {
        mUserDataService = new UserDataService();
    }


    /// <summary>
    /// 根据用户ID获取用户数据
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="callBackView"></param>
    /// <returns></returns>
    public UserDataBean GetUserData(string userId, IUserDataView callBackView)
    {
        if (CheckUtil.StringIsNull(userId))
        {
            callBackView.DeleteUserDataFail(UserDataFailEnum.NoUserId);
            return null;
        }
        return mUserDataService.QueryDataByUserId(userId);
    }

    /// <summary>
    /// 创建一个全新的用户数据
    /// </summary>
    /// <returns></returns>
    public UserDataBean CreateUserData()
    {
        UserDataBean userData = new UserDataBean();
        List<UserItemLevelBean> itemLevelList = new List<UserItemLevelBean>();
        for(int i = 0; i < 1; i++)
        {
            UserItemLevelBean itemLevelData = new UserItemLevelBean();
            itemLevelData.level = 1;
            itemLevelData.goodsNumber = 1;
            itemLevelList.Add(itemLevelData);
        }
        userData.userId = "UserId_" + SystemUtil.GetUUID(SystemUtil.UUIDTypeEnum.N);
        userData.itemLevelList = itemLevelList;
        userData = mUserDataService.SaveData(userData);
        return userData;
    }

    /// <summary>
    /// 保存用户数据
    /// </summary>
    /// <param name="userData"></param>
    /// <returns></returns>
    public UserDataBean SaveUserData(UserDataBean userData,IUserDataView callBackView)
    {
        if (userData == null)
        {
            callBackView.SaveUserDataFail(UserDataFailEnum.NoData);
            return null;
        }
        if (CheckUtil.StringIsNull(userData.userId))
        {
            callBackView.SaveUserDataFail(UserDataFailEnum.NoUserId);
            return null;
        }
        mUserDataService.SaveData(userData);
        return userData;
    }

    /// <summary>
    /// 删除用户数据
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="callBackView"></param>
    /// <returns></returns>
    public UserDataBean DeleteUserData(string userId, IUserDataView callBackView)
    {
        if (CheckUtil.StringIsNull(userId))
        {
            callBackView.DeleteUserDataFail(UserDataFailEnum.NoUserId);
            return null;
        }
        return mUserDataService.DeleteData(userId);
    }

}