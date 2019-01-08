using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class LevelSkillsBean 
{
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
}