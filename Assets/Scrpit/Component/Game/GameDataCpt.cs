using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;

public class GameDataCpt : BaseObservable<IGameDataCallBack>, IUserDataView, IGameScenesView, IGameSkillsView
{

    //用户数据
    public UserDataBean userData;
    //场景数据
    public List<LevelScenesBean> listScenesData;
    //所有技能数据
    public List<LevelSkillsBean> listSkillsData;

    //用户数据管理
    private UserDataController mUserDataController;
    private GameScenesController mGameScenesController;
    private GameSkillsController mGameSkillsController;

    private void Awake()
    {
        mUserDataController = new UserDataController(this, this);
        mGameScenesController = new GameScenesController(this, this);
        mGameSkillsController = new GameSkillsController(this, this);

        mUserDataController.GetUserData(GameCommonInfo.gameUserId);
        mGameScenesController.GetAllGameScenesData();
        mGameSkillsController.GetAllLevelSkill();
    }

    private void FixedUpdate()
    {
        float updateNumber = 1 / Time.fixedDeltaTime;
        if (updateNumber <= 0)
            return;
        double tempGrow = userData.userGrow / updateNumber;
        userData.userScore += tempGrow;
        CheckLevel();
    }

    /// <summary>
    /// 检测等级
    /// </summary>
    public void CheckLevel()
    {
        if (listScenesData != null && userData != null)
            for (int i = 0; i < listScenesData.Count; i++)
            {
                LevelScenesBean itemData = listScenesData[i];
                if (userData.userScore >= itemData.goods_sell_price && userData.userLevel < itemData.level)
                {
                    userData.userLevel = itemData.level;

                    //通知所有观察者
                    List<IGameDataCallBack> listObserver = GetAllObserver();
                    for (int f = 0; f < listObserver.Count; f++)
                    {
                        IGameDataCallBack itemObserver = listObserver[f];
                        itemObserver.LevelChange(userData.userLevel);
                    }
                }
            }
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
    /// 根据等级获取相信技能列表
    /// </summary>
    /// <param name="level">技能等级</param>
    /// <param name="userLevel">技能解锁等级</param>
    /// <returns></returns>
    public List<LevelSkillsBean> GetSkillsListByLevel(int level, int userLevel)
    {
        List<LevelSkillsBean> tempListData = new List<LevelSkillsBean>();
        if (CheckUtil.ListIsNull(listSkillsData))
        {
            return tempListData;
        }
        for (int i = 0; i < listSkillsData.Count; i++)
        {
            LevelSkillsBean itemData = listSkillsData[i];
            if (itemData.level == level && userLevel >= itemData.unlock_level)
            {
                tempListData.Add(itemData);
            }
        }
        return tempListData;
    }

    /// <summary>
    /// 根据用户等级获取场景数据
    /// </summary>
    /// <param name="userLevel"></param>
    /// <returns></returns>
    public List<LevelScenesBean> GetScenesListByLevel(int userLevel)
    {
        List<LevelScenesBean> tempListData = new List<LevelScenesBean>();
        if (CheckUtil.ListIsNull(listScenesData))
            return tempListData;
        for (int i = 0; i < listScenesData.Count; i++)
        {
            LevelScenesBean itemData = listScenesData[i];
            if (userLevel >= itemData.level)
            {
                tempListData.Add(itemData);
            }
        }
        return tempListData;
    }

    /// <summary>
    /// 根据等级获取等级数据
    /// </summary>
    /// <param name="level"></param>
    public UserItemLevelBean GetUserItemLevelDataByLevel(int level)
    {
        if (userData == null || CheckUtil.ListIsNull(userData.itemLevelList))
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

    /// <summary>
    /// 增加分数
    /// </summary>
    /// <param name="score"></param>
    public void AddScore(double score)
    {
        userData.userScore += score;
        //通知所有观察者
        List<IGameDataCallBack> listObserver = GetAllObserver();
        foreach (IGameDataCallBack itemObserver in listObserver)
        {
            itemObserver.ScoreChange(score);
        }
    }

    /// <summary>
    /// 移除分数
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    public bool RemoveScore(double score)
    {
        if (userData.userScore < score)
        {
            return false;
        }
        else
        {
            userData.userScore -= score;
            return true;
        }
    }

    /// <summary>
    /// 增加对应等级商品数量
    /// </summary>
    /// <param name="level"></param>
    /// <param name="number"></param>
    public void AddLevelGoods(int level, int number)
    {
        List<UserItemLevelBean> listUserLevelData = userData.itemLevelList;
        if (CheckUtil.ListIsNull(listUserLevelData))
            listUserLevelData = new List<UserItemLevelBean>();
        for (int i = 0; i < listUserLevelData.Count; i++)
        {
            UserItemLevelBean itemData = listUserLevelData[i];
            if (itemData.level.Equals(level))
            {
                itemData.goodsNumber += number;
                //通知所有观察者
                List<IGameDataCallBack> listObserver = GetAllObserver();
                foreach (IGameDataCallBack itemObserver in listObserver)
                {
                    itemObserver.GoodsNumberChange(level, number);
                }
            }
        }
        RefreshData();
    }

    /// <summary>
    /// 增加场景数量
    /// </summary>
    /// <param name="level"></param>
    /// <param name="number"></param>
    public void AddLevelSpace(int level, int number)
    {
        List<UserItemLevelBean> listUserLevelData = userData.itemLevelList;
        if (CheckUtil.ListIsNull(listUserLevelData))
            listUserLevelData = new List<UserItemLevelBean>();
        bool hasData = false;
        for (int i = 0; i < listUserLevelData.Count; i++)
        {
            UserItemLevelBean itemData = listUserLevelData[i];
            if (itemData.level.Equals(level))
            {
                hasData = true;
                itemData.spaceNumber += number;
            }
        }
        if (!hasData)
        {
            UserItemLevelBean userItemLevelBean = new UserItemLevelBean();
            userItemLevelBean.level = level;
            userItemLevelBean.spaceNumber = number;
            userData.itemLevelList.Add(userItemLevelBean);
        }
        //通知所有观察者
        List<IGameDataCallBack> listObserver = GetAllObserver();
        foreach (IGameDataCallBack itemObserver in listObserver)
        {
            itemObserver.SpaceNumberChange(level, number);
        }
    }

    /// <summary>
    /// 刷新数据
    /// </summary>
    public void RefreshData()
    {
        List<UserItemLevelBean> listUserLevelData = userData.itemLevelList;
        userData.userGrow = 0;
        if (CheckUtil.ListIsNull(listUserLevelData))
        {
            return;
        }
        for (int i = 0; i < listUserLevelData.Count; i++)
        {
            UserItemLevelBean itemData = listUserLevelData[i];
            userData.userGrow += (itemData.itemGrow * itemData.goodsNumber);
        }
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

    public void GetScenesDataSuccessByUserData(LevelScenesBean levelScenesData, UserItemLevelBean itemLevelData)
    {

    }

    public void GetAllScenesDataSuccess(List<LevelScenesBean> listScenesData)
    {
        this.listScenesData = listScenesData;
    }

    public void GetAllLevelSkillsDataSuccess(List<LevelSkillsBean> listSkillsData)
    {
        this.listSkillsData = listSkillsData;
    }

    public void GetAllLevelSkillsDataFail()
    {

    }
    #endregion



}