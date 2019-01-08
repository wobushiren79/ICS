using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameSkillsItem :BaseMonoBehaviour
{
    public Text tvName;

    public GameObject listDetailsObj;
    public GameObject itemDetailsModel;

    public GameDataCpt gameDataCpt;

    public LevelScenesBean levelScenesData;
    public List<LevelSkillsBean> listSkills;

    public void SetData(LevelScenesBean levelScenesData)
    {
        this.levelScenesData = levelScenesData;
        this.listSkills = gameDataCpt.GetSkillsListByLevel(levelScenesData.level, gameDataCpt.userData.userLevel);
        if (tvName != null)
            tvName.text = levelScenesData.goods_name;
        if (listSkills == null|| itemDetailsModel==null|| itemDetailsModel==null)
            return;
        for (int i = 0; i < this.listSkills.Count; i++)
        {
            LevelSkillsBean itemData= listSkills[i];
            GameObject itemObj=  Instantiate(itemDetailsModel, itemDetailsModel.transform);
            itemObj.transform.SetParent(listDetailsObj.transform);
            itemObj.SetActive(true);
            GameSkillsDetailsItem itemCpt= itemObj.GetComponent<GameSkillsDetailsItem>();
            if (itemCpt != null)
                itemCpt.SetData(itemData);
        }
    }

    private void Start()
    {

    }
}