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
    //用户商品等级（默认1级）
    public int goodsLevel = 1;
    //用户分数等级
    public int scoreLevel = 1;
    //用户分数
    public double userScore;
    //用户分数增量(每秒增量)
    public double userGrow;
    //不同等级的数据
    public List<UserItemLevelBean> listUserLevelData;
    //点击数据
    public UserItemLevelBean clickData;
    //用户技能列表
    public List<long> listSkillsData;
}