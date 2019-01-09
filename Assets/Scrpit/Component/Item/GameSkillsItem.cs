using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

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
        this.listSkills = gameDataCpt.GetSkillsListByLevel(levelScenesData.level, gameDataCpt.userData.goodsLevel);
        if (tvName != null)
            tvName.text = levelScenesData.goods_name;
        if (listSkills == null|| itemDetailsModel==null|| itemDetailsModel==null)
            return;
        for (int i = 0; i < this.listSkills.Count; i++)
        {
            LevelSkillsBean itemData= listSkills[i];
            GameObject itemObj=  Instantiate(itemDetailsModel, itemDetailsModel.transform);
            itemObj.transform.SetParent(listDetailsObj.transform);
            itemObj.transform.localScale=new Vector3(0,0,0);
            itemObj.transform.DOScale(new Vector3(1,1,1),0.5f).SetDelay(i * 0.1f);
            itemObj.SetActive(true);
            GameSkillsDetailsItem itemCpt= itemObj.GetComponent<GameSkillsDetailsItem>();
            if (itemCpt != null)
                itemCpt.SetData(itemData, levelScenesData);
        }
    }

    private void Start()
    {

    }
}