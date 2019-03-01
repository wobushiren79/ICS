using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class UIRebirthTalentCpt : BaseUIComponent
{
    public GameDataCpt gameDataCpt;

    public GameObject listTalentContent;
    public GameObject itemTalentModel;

    private void Start()
    {
        InitData();
    }

    public void InitData()
    {
        if (gameDataCpt.userData.rebirthData==null)
        {
            gameDataCpt.userData.rebirthData = new RebirthBean();
        }
        if (gameDataCpt.userData.rebirthData.listRebirthTalentData==null)
        {
            gameDataCpt.userData.rebirthData.listRebirthTalentData = new List<RebirthTalentItemBean>();
        }
        List<TalentInfoBean> listTalentData = gameDataCpt.listTalentData;
        if (listTalentData != null)
        {
            for(int i = 0; i < listTalentData.Count; i++)
            {
                TalentInfoBean itemTalentData = listTalentData[i];
                CreateItemTalent(itemTalentData,gameDataCpt.userData.rebirthData.GetRebirthTalentDataById(itemTalentData.id));
            }
        }
    }

    public void CreateItemTalent(TalentInfoBean talentInfoBean, RebirthTalentItemBean rebirthTalentItemBean)
    {
        if (listTalentContent == null || itemTalentModel == null)
            return;
        GameObject talentObj = Instantiate(itemTalentModel, itemTalentModel.transform);
        talentObj.SetActive(true);
        talentObj.transform.SetParent(listTalentContent.transform);
        talentObj.transform.localPosition = new Vector3((float)talentInfoBean.position_x, (float)talentInfoBean.position_y, talentObj.transform.position.y);

        RebirthTalentItemCpt talentItem= talentObj.GetComponent<RebirthTalentItemCpt>();
        talentItem.SetData(talentInfoBean, rebirthTalentItemBean);
        talentObj.transform.DOScale(new Vector3(0, 0, 0),1f).From();
    }
}