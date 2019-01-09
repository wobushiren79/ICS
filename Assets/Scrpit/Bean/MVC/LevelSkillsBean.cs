using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class LevelSkillsBean 
{
    public long id;
    //图标key
    public string icon_key;
    //等级
    public int level;
    //价格
    public double price;
    //是否有效
    public int valid;
    //名字
    public string name;
    //介绍
    public string introduction;
    //解锁等级
    public int unlock_level;

    //增长生成效率
    public double add_grow;
    //增加个数
    public int add_number;
    //增长率
    public double add_times;
}