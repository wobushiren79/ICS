using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[Serializable]
public class AchievementBean 
{
    //达到过的最高用户分数
    public double maxUserScore;
    //点击次数
    public long clickTime;
    //用户解锁过的技能列表
    public List<long> unlockSkillsList;
    //解锁的等级数据
    public List<AchievementItemLevelBean> listLevelData;
}