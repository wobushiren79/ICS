using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameAchievementSkillCpt : BaseMonoBehaviour
{
    public GameObject headingObj;
    public Text tvHeading;

    public GameObject itemModel;
    public GameObject listContent;

    public GameDataCpt gameDataCpt;

    private void Start()
    {
        if (tvHeading)
            tvHeading.text = GameCommonInfo.GetTextById(62);
    }

    public void RefreshData()
    {
        CptUtil.RemoveChildsByActive(listContent.transform);
        if (itemModel==null
            || listContent==null
            || gameDataCpt == null
            || gameDataCpt.userData.userAchievement==null
            ||gameDataCpt.userData.userAchievement == null 
            ||CheckUtil.ListIsNull(gameDataCpt.userData.userAchievement.unlockSkillsList))
        {
            headingObj.SetActive(false);
            return;
        }
        else
        {
            tvHeading.gameObject.SetActive(true);
            List<long> listAch = gameDataCpt.userData.userAchievement.unlockSkillsList;
            List<LevelSkillsBean> listSkills = gameDataCpt.GetSkillsListByIds(listAch);
            for(int i = 0; i < listSkills.Count; i++)
            {
                LevelSkillsBean itemSkill= listSkills[i];
                CreateItem(itemSkill);
            }
        }
    }

    private void CreateItem(LevelSkillsBean levelSkillsBean)
    {
        if (levelSkillsBean.icon_key.Contains("all"))
        {
            return;
        }
        GameObject itemObj=  Instantiate(itemModel, itemModel.transform);
        itemObj.SetActive(true);
        itemObj.transform.SetParent(listContent.transform);
        GameAchSkillsDetailsItem ItemAchSkill= itemObj.GetComponent<GameAchSkillsDetailsItem>();
        LevelScenesBean levelScenesBean=  gameDataCpt.GetScenesByLevel(levelSkillsBean.level);
        ItemAchSkill.SetData(levelSkillsBean, levelScenesBean);
    }
}