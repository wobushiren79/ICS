using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GameOilShowCpt : PopupReplyView
{
    public Text tvOilNumber;
    public Image ivOilBarrel;
    public Image ivOilContent;

    public GameDataCpt gameDataCpt;

    private void Start()
    {
        
    }

    private void Update()
    {
        UpdateOilData();
    }

    public override void ClosePopup()
    {
      
    }

    public override void OpenPopup()
    {
        string titleStr = GameCommonInfo.GetTextById(96);
        string descriptionStr = GameCommonInfo.GetTextById(97);
        string otherStr = GameCommonInfo.GetTextById(104)+"\n" + GameCommonInfo.GetTextById(105)+(int)(gameDataCpt.userData.chiliOil % 1*100)+"%";
        infoPopupView.SetInfoData(ivOilBarrel.sprite, titleStr, "",null, descriptionStr, otherStr);
    }

    public void UpdateOilData()
    {
        if (gameDataCpt == null)
            return;
        //设置辣椒油数量
        if (tvOilNumber != null)
            tvOilNumber.text = (int)gameDataCpt.userData.chiliOil+"";
        float curOil = gameDataCpt.userData.chiliOil % 1;
        ivOilContent.transform.localScale=new Vector3(1, curOil, 1);
    }
}