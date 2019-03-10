using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class UserDataService : BaseDataStorageImpl<UserDataBean>
{
    private readonly string mSaveFileName;

    public UserDataService()
    {
        mSaveFileName = "UserData";
    }

    /// <summary>
    /// 根据用户ID查询用户数据
    /// </summary>
    /// <param name="userId"></param>
    public UserDataBean QueryDataByUserId(string userId)
    {
        List<UserDataBean> listUserData = QueryAllData();
        if (listUserData == null)
            return null;
        foreach (UserDataBean itemUserData in listUserData)
        {
            if (itemUserData.userId.Equals(userId))
            {
                return itemUserData;
            }
        }
        return null;
    }

    /// <summary>
    /// 查询所有保存的数据
    /// </summary>
    /// <returns></returns>
    public List<UserDataBean> QueryAllData()
    {
        return BaseStartLoadDataForList(mSaveFileName);
    }


    /// <summary>
    /// 保存用户数据
    /// </summary>
    /// <param name="userData"></param>
    public UserDataBean SaveData(UserDataBean userData)
    {
        userData.offlineTime = TimeUtil.GetNowTime();
        List<UserDataBean> listUserData = QueryAllData();
        //如果没有数据，则添加数据
        if (listUserData == null|| listUserData.Count==0)
        {
            listUserData = new List<UserDataBean>
            {
                userData
            };
            BaseStartSaveDataForList(mSaveFileName,listUserData);
            return userData;
        }
        bool hasSameData = false;
        for(int i=0;i< listUserData.Count; i++)
        {
            UserDataBean itemData = listUserData[i];
            if (itemData.userId.Equals(userData.userId))
            {
                listUserData[i] = userData;
                hasSameData = true;
            }
        }
        if (!hasSameData)
        {
            listUserData.Add(userData);
        }
        BaseStartSaveDataForList(mSaveFileName, listUserData);
        return userData;
    }

    /// <summary>
    /// 根据用户ID删除用户数据
    /// </summary>
    public UserDataBean DeleteData(string userId)
    {
        List<UserDataBean> listUserData = QueryAllData();
        UserDataBean tempData=null;
        //如果没有数据
        if (listUserData == null || listUserData.Count == 0)
            return null;
        for (int i = 0; i < listUserData.Count; i++)
        {
            UserDataBean itemData = listUserData[i];
            if (itemData.userId.Equals(userId))
            {
                tempData = itemData;
                listUserData.Remove(itemData);
                i--;
            }
        }
        BaseStartSaveDataForList(mSaveFileName, listUserData);
        return tempData;
    }

}