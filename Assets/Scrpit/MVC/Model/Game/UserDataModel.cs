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
    /// 获取所有用户数据
    /// </summary>
    /// <returns></returns>
    public List<UserDataBean> GetAllUserData()
    {
        return mUserDataService.QueryAllData();
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
    public UserDataBean CreateUserData(string userName)
    {
        UserDataBean userData = new UserDataBean();
        List<UserItemLevelBean> itemLevelList = new List<UserItemLevelBean>();

        

        //查询等级1的数据 
        List<LevelScenesBean> listLevelData = mLevelScenesService.QueryDataByLevel(new int[] {0,1});
        if (CheckUtil.ListIsNull(listLevelData))
            return null;
        double totalGrow = 0;
        for(int i = 0; i < listLevelData.Count; i++)
        {
            //添加一个数据
            LevelScenesBean initLevelData = listLevelData[i];
            UserItemLevelBean itemLevelData = new UserItemLevelBean();
            itemLevelData.level = initLevelData.level;
            itemLevelData.goodsNumber = 1;
            itemLevelData.spaceNumber = 1;
            itemLevelData.itemGrow = initLevelData.item_grow;
            if (initLevelData.level==0)
            {
                userData.clickData = itemLevelData;
            }
            else
            {
                itemLevelList.Add(itemLevelData);
                totalGrow += itemLevelData.itemGrow;
            }
        }

        userData.userId = "USERID_" + SystemUtil.GetUUID(SystemUtil.UUIDTypeEnum.N);
        userData.listUserLevelData = itemLevelList;
        userData.userGrow = totalGrow;
        userData.userTimes = 1;
        userData.userName = userName;
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