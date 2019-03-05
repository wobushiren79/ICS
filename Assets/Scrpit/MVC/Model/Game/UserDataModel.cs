using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

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
        
        //查询等级0 1的数据 
        List<LevelScenesBean> listLevelData = mLevelScenesService.QueryDataByLevel(new int[] {0,1});
        if (CheckUtil.ListIsNull(listLevelData))
            return null;
        double totalGrow = 0;
        AchievementItemLevelBean tempLevelAch = new AchievementItemLevelBean();
        for (int i = 0; i < listLevelData.Count; i++)
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

                tempLevelAch.level = initLevelData.level;
                tempLevelAch.totalNumber = itemLevelData.goodsNumber;
            }

        }

        userData.userId = "USERID_" + SystemUtil.GetUUID(SystemUtil.UUIDTypeEnum.N);
        userData.listUserLevelData = itemLevelList;
        userData.userGrow = totalGrow;
        userData.userTimes = 1;
        userData.userName = userName;
 
        userData.userAchievement = new AchievementBean();
        userData.userAchievement.unlockSkillsList = new List<long>();
        userData.userAchievement.listLevelData = new List<AchievementItemLevelBean>();
        userData.userAchievement.listLevelData.Add(tempLevelAch);

        userData.rebirthData = new RebirthBean();
        userData = mUserDataService.SaveData(userData);
        return userData;
    }

    /// <summary>
    /// 重生用户数据
    /// </summary>
    /// <param name="userData"></param>
    /// <param name="userDataView"></param>
    /// <returns></returns>
    public UserDataBean RebirthUserData(UserDataBean userData, IUserDataView userDataView)
    {
        if (CheckUtil.StringIsNull(userData.userId))
        {
            userDataView.ChangeUserDataFail(UserDataFailEnum.NoUserId);
            return userData;
        }
        List<UserItemLevelBean> itemLevelList = userData.listUserLevelData ;
        List<RebirthTalentItemBean> rebirthTalentList= userData.rebirthData.listRebirthTalentData;
        double userTimeAdd = 1;
        if (rebirthTalentList != null)
        {
            //不同等级重生
            for (int f = 0; f < itemLevelList.Count; f++)
            {
                UserItemLevelBean itemLevelBean = itemLevelList[f];
                itemLevelBean.itemTimes = 1;
                itemLevelBean.itemGrow = 0;
                itemLevelBean.goodsNumber = 0;
                itemLevelBean.spaceNumber = 0;
                if (itemLevelBean.level == 1)
                {
                    itemLevelBean.goodsNumber = 1;
                    itemLevelBean.spaceNumber = 1;
                    List<LevelScenesBean> levelScenesBeans = mLevelScenesService.QueryDataByLevel(1);
                    if (!CheckUtil.ListIsNull(levelScenesBeans))
                    {
                        itemLevelBean.itemGrow = levelScenesBeans[0].item_grow;
                    }
                }

                for (int i = 0; i < rebirthTalentList.Count; i++)
                {
                    RebirthTalentItemBean rebirthTalentItem = rebirthTalentList[i];
                    if(rebirthTalentItem.add_type== itemLevelBean.level)
                    {
                        itemLevelBean.itemTimes += rebirthTalentItem.total_add;
                        break;
                    }
                }
            }
            //特殊修改
            for (int i = 0; i < rebirthTalentList.Count; i++)
            {
                RebirthTalentItemBean rebirthTalentItem = rebirthTalentList[i];
                //手指点击修改
                if (rebirthTalentItem.add_type == 101)
                {
                    userData.clickData.itemGrow = 1;
                    userData.clickData.itemTimes = 1;
                    List<LevelScenesBean> levelScenesBeans= mLevelScenesService.QueryDataByLevel(0);
                    if (!CheckUtil.ListIsNull(levelScenesBeans))
                    {
                        userData.clickData.itemGrow = levelScenesBeans[0].item_grow;
                    }
                    userData.clickData.itemTimes += rebirthTalentItem.total_add;
                }
                if (rebirthTalentItem.add_type == 201)
                {
                    userTimeAdd += rebirthTalentItem.total_add;
                }
            }
        }
        userData.listUserLevelData = itemLevelList;
        userData.userScore = 0;
        userData.userGrow = 0;
        userData.userTimes = userTimeAdd;
        userData.goodsLevel = 1;
        userData.scoreLevel = 1;
        userData.listSkillsData = new List<long>();
        userData.rebirthData.rebirthNumber += 1;
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