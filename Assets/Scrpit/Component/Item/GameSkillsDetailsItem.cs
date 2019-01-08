using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;


public class GameSkillsDetailsItem : PopupReplyView
{
    public Image ivIcon;
    public Image ivBorder;
    public Button btSubmit;
    public LevelSkillsBean levelSkillsBean;
    public GameDataCpt gameDataCpt;
    private bool hasSkills=false;

    public void SetData(LevelSkillsBean levelSkillsBean)
    {
        this.levelSkillsBean = levelSkillsBean;
        if (gameDataCpt == null)
            return;
        if (ivIcon != null)
            ivIcon.sprite = gameDataCpt.GetIconByKey(levelSkillsBean.icon_key);
         hasSkills =  gameDataCpt.HasSkillsById(levelSkillsBean.id);
        if (hasSkills)
        {

        }
        else
        {
              ivBorder.color = new Color(0.5f, 0.5f, 0.5f);
        }
        btSubmit.onClick.AddListener(BTSkillsBuyOnClick);
    }

    public void BTSkillsBuyOnClick()
    {
        transform.DOKill();
        transform.localScale = new Vector3(1,1, 1);
        transform.localRotation = new Quaternion();
        transform.DOPunchScale(new Vector3(0.5f,0.5f,1),1);
        transform.DOShakeRotation(1,new Vector3(0, 0,360));

        if (!hasSkills)
        {
            gameDataCpt.userData.userSkillsList.Add(levelSkillsBean.id);
            ivBorder.color = new Color(1, 1, 1);
            hasSkills = true;
        }
    }


    public override void ClosePopup()
    {
 
    }

    public override void OpenPopup()
    {
        if (ivIcon == null|| levelSkillsBean==null)
            return;
        string nameStr = levelSkillsBean.name;
        string remarkStr = "remark";
        string priceStr = levelSkillsBean.price+"";
        string descriptionStr = levelSkillsBean.introduction;
        infoPopupView.SetInfoData(ivIcon.sprite, nameStr, remarkStr, priceStr, descriptionStr,null);
    }
}