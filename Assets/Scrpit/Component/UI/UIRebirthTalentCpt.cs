using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class UIRebirthTalentCpt : BaseUIComponent
{
    public Button btBack;
    public Button btRebirth;
    public Text tvBack;
    public Text tvRebirth;

    public Text tvTitle;
    public Text tvRebirthNumber;
    public Text tvPoints;

    public GameDataCpt gameDataCpt;

    public GameObject listTalentContent;
    public GameObject itemTalentModel;

    private void Start()
    {
        if (btBack != null)
            btBack.onClick.AddListener(BackOnClick);
        if (btRebirth != null)
            btRebirth.onClick.AddListener(RebirthOnClick);
        if (tvBack != null)
            tvBack.text = GameCommonInfo.GetTextById(78);
        if (tvRebirth != null)
            tvRebirth.text = GameCommonInfo.GetTextById(77);
        if (tvTitle != null)
            tvTitle.text = GameCommonInfo.GetTextById(75);
        if (tvRebirthNumber != null)
            tvRebirthNumber.text = GameCommonInfo.GetTextById(76)+"\n"+gameDataCpt.userData.rebirthData.rebirthNumber;
        InitData();
    }

    private void Update()
    {
        if (tvPoints != null)
            tvPoints.text = "x" + gameDataCpt.userData.rebirthData.rebirthChili;
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

    public void RebirthOnClick()
    {

    }

    public void BackOnClick()
    {
        SceneUtil.SceneChange("GameScene");
    }

    /// <summary>
    /// 创建Item
    /// </summary>
    /// <param name="talentInfoBean"></param>
    /// <param name="rebirthTalentItemBean"></param>
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