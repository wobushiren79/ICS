using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[Serializable]
public class RebirthBean 
{
    //转生次数
    public int rebirthNumber;
    //天赋数据
    public List<RebirthTalentItemBean> listRebirthTalentData;

    /// <summary>
    /// 根据天赋ID获取天赋数据
    /// </summary>
    /// <param name="talentId"></param>
    /// <returns></returns>
    public RebirthTalentItemBean GetRebirthTalentDataById(long talentId)
    {
        if (listRebirthTalentData == null)
            listRebirthTalentData = new List<RebirthTalentItemBean>();
        for(int i=0;i< listRebirthTalentData.Count; i++)
        {
            RebirthTalentItemBean itemData= listRebirthTalentData[i];
            if (itemData.talent_id == talentId)
            {
                return itemData;
            }
        }
        return null;
    }
}