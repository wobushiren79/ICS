using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class TalentInfoBean 
{
    public long id;
    public int valid;
    public double position_x;//坐标X
    public double position_y;//坐标Y
    public int unlock_level;//解锁等级
    public int add_type;//类型 0 手指；1-15 等级；
    public string icon_key;//图标key

    public string name;
    public string content;
}