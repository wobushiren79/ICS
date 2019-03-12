using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameAchievementGeneralCpt : BaseMonoBehaviour
{
    public Text tvHeading;

    public GameObject itemModel;
    public GameObject listContent;

    public GameDataCpt gameDataCpt;

    private void Start()
    {
        if (tvHeading)
            tvHeading.text = GameCommonInfo.GetTextById(61);
    }

    public void RefreshData()
    {
        if (itemModel == null || gameDataCpt == null)
            return;
        if (gameDataCpt.userData.rebirthData == null)
            gameDataCpt.userData.rebirthData = new RebirthBean();
        if (gameDataCpt.userData.gameTime == null)
            gameDataCpt.userData.gameTime = new TimeBean();
        CptUtil.RemoveChildsByActive(listContent.transform);
        CreateItem(GameCommonInfo.GetTextById(63) + "：", GameCommonInfo.GetPriceStr(gameDataCpt.userData.userScore), "sacuce_list_0");
        CreateItem(GameCommonInfo.GetTextById(65) + "：", GameCommonInfo.GetPriceStr(gameDataCpt.userData.userAchievement.maxUserScore), "sacuce_list_0");
        CreateItem(GameCommonInfo.GetTextById(67) + "：", gameDataCpt.userData.userAchievement.clickTime + "");

        TimeBean gameTime = gameDataCpt.userData.gameTime;
        string gameTimeStr = gameTime.day + GameCommonInfo.GetTextById(101) + gameTime.hour + GameCommonInfo.GetTextById(100) + gameTime.minute + GameCommonInfo.GetTextById(99) + gameTime.second + GameCommonInfo.GetTextById(98);
        CreateItem(GameCommonInfo.GetTextById(106) + "：", gameTimeStr);

        CreateItem(GameCommonInfo.GetTextById(64) + "：", gameDataCpt.userData.rebirthData.rebirthNumber + "");
        CreateItem(GameCommonInfo.GetTextById(88) + "：", gameDataCpt.userData.rebirthData.rebirthChili + "", "rebirth_chili");
        CreateItem(GameCommonInfo.GetTextById(96) + "：", (int)gameDataCpt.userData.chiliOil + "", "oil_barrel");
        List<LevelScenesBean> listScenesData= gameDataCpt.listScenesData;
        if (listScenesData!=null)
        {
            if (gameDataCpt.userData.rebirthData.rebirthNumber <= 0)
            {

            }
            else
            {

            }
            if (gameDataCpt.userData.userAchievement != null || gameDataCpt.userData.userAchievement.listLevelData != null) {
                List<AchievementItemLevelBean> listAchievementData = gameDataCpt.userData.userAchievement.listLevelData;
                for (int i = 0; i < listScenesData.Count; i++)
                {
                    LevelScenesBean itemScenes = listScenesData[i];
                    AchievementItemLevelBean itemAch=null;
                    if (listAchievementData == null)
                        continue;
                    for (int f = 0;f < listAchievementData.Count; f++)
                    {
                        AchievementItemLevelBean tempAch= listAchievementData[f];
                        if (tempAch.level== itemScenes.level)
                        {
                            itemAch = tempAch;
                            break;
                        }
                    }
                    if (itemAch != null)
                    {
                        CreateItem(GameCommonInfo.GetTextById(66) + itemScenes.goods_name + "：", itemAch.totalNumber+"","store_goods_"+itemScenes.level);
                    }    
                }
            }
        }
    }

    private void CreateItem(string title, string content)
    {
        CreateItem(title, content, null);
    }

    private void CreateItem(string title,string content,string iconKey)
    {
        GameObject itemObj = Instantiate(itemModel, itemModel.transform);
        itemObj.SetActive(true);
        itemObj.transform.SetParent(listContent.transform);
        Text tvTitle= CptUtil.GetCptInChildrenByName<Text>(itemObj,"Title");
        tvTitle.text = title;
        Text tvContent = CptUtil.GetCptInChildrenByName<Text>(itemObj, "ContentText");
        tvContent.text = content;
        Image ivContent = CptUtil.GetCptInChildrenByName<Image>(itemObj, "ContentIcon");
        if (iconKey == null)
        {
            ivContent.gameObject.SetActive(false);
        }
        else
        {
            ivContent.gameObject.SetActive(true);
            Sprite iconSp= gameDataCpt.GetIconByKey(iconKey);
            ivContent.sprite = iconSp;
        }
    }
}