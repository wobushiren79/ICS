using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
public class GameAchSkillsDetailsItem : GameSkillsDetailsItem
{

    public override void SetData(LevelSkillsBean levelSkillsBean, LevelScenesBean levelScenesBean)
    {
        this.levelScenesBean = levelScenesBean;
        this.levelSkillsBean = levelSkillsBean;
        if (gameDataCpt == null)
            return;
        if (ivIcon != null)
            ivIcon.sprite = gameDataCpt.GetIconByKey(levelSkillsBean.icon_key);
    }

}