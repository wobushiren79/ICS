using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class UserItemLevelBean 
{
    //当前item等级
    public int level;
    //地皮等级  每1级增加一块大小
    public int spaceNumber = 0;
    //购买的商品数量
    public int goodsNumber = 0;
    //单个物品的增长率
    public double itemGrow = 0;
    //增长倍率
    public double itemTimes = 1;

}