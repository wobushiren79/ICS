using UnityEngine;
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
    private LevelScenesService mLevelScenesService;

    public override void InitData()
    {
        mUserDataService = new UserDataService();
        mLevelScenesService = new LevelScenesService();
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

        //查询等级1的数据 
        List<LevelScenesBean> listLevelData = mLevelScenesService.QueryDataByLevel(1);
        if (CheckUtil.ListIsNull(listLevelData))
            return null;
        LevelScenesBean initLevelData = listLevelData[0];
        //添加一个数据
        UserItemLevelBean itemLevelData = new UserItemLevelBean();
        itemLevelData.level = initLevelData.level;
        itemLevelData.goodsNumber = 1;
        itemLevelData.spaceNumber = 1;
        itemLevelData.itemGrow = initLevelData.item_grow;
        itemLevelList.Add(itemLevelData);

        userData.userId = "UserId_" + SystemUtil.GetUUID(SystemUtil.UUIDTypeEnum.N);
        userData.itemLevelList = itemLevelList;
        userData.userGrow = initLevelData.item_grow;
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