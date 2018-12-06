using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[Serializable]
public class UserDataBean 
{
    //用户ID
    public string userId;
    //用户名称
    public string userName;
    //用户等级（默认1级）
    public int userLevel = 1;
    //不同等级的数据
    public List<UserItemLevelBean> itemLevelList;
}