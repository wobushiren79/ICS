using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class OilInfoBean
{
    public long id;
    //图标key
    public string icon_key;
    //解锁等级
    public int unlock_level;
    //价格
    public double price;
    //是否有效
    public int valid;
    //名字
    public string name;
    //内容
    public string content;
   //备注
    public string other;
}