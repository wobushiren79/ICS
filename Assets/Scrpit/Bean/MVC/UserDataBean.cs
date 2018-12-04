using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class UserDataBean 
{
    //用户ID
    public string userId;
    //用户等级（默认1级）
    public int userLevel = 1;
}