using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class NewsInfoBean
{
    //新闻ID
    public long news_id;
    //新闻等级
    public int level;
    //内容
    public string content;
    //作者
    public string author;
    //是否有效
    public int valid;
}