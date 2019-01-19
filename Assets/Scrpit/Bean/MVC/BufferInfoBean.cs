using UnityEngine;
using UnityEditor;
using System;
[Serializable]
public class BufferInfoBean 
{
    public long id;
    public long buffer_id;
    public int level;
    public int time;//持续时间
    public string icon_key;//图标KEY
    public string name;//名字
    public string content;//内容
}