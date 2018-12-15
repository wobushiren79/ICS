using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIGameLevelInfoCpt : BaseUIComponent,IGameDataCallBack
{
    //游戏数据控制
    public GameDataCpt gameDataCpt;
    //游戏镜头控制
    public GameCameraCpt gameCameraCpt;

    //等级容器
    public GameObject levelContent;
    public GameObject levelItemModel;

    public List<Sprite> levelIconList;

    private void Start()
    {
        if (gameDataCpt != null)
            gameDataCpt.AddObserver(this);
        CreateLevelButton();
    }

    public void CreateLevelButton()
    {
        if (gameDataCpt == null|| levelItemModel == null|| levelContent==null|| levelItemModel == null)
            return;
        //删除原数据
        CptUtil.RemoveChildsByActive(levelContent.transform);
        List<UserItemLevelBean> userLevelDataList =  gameDataCpt.userData.itemLevelList;
        if (CheckUtil.ListIsNull(userLevelDataList))
            return;
        for(int i=0;i< userLevelDataList.Count; i++)
        {
            UserItemLevelBean levelData= userLevelDataList[i];
            GameObject levelObj =   Instantiate(levelItemModel, levelItemModel.transform);
            levelObj.SetActive(true);
            levelObj.transform.parent = levelContent.transform;
            //设置等级图片
            Image itemImage = levelObj.GetComponent<Image>();
            itemImage.sprite = levelIconList[levelData.level-1];
            //设置按钮
            Button itemButton = levelObj.GetComponent<Button>();
            itemButton.onClick.AddListener(delegate() {
                gameCameraCpt.ChangePerspectiveByLevel(levelData.level,0);
            });
    
        }
    }

    #region 游戏数据回调
    public void ObserbableUpdate(int type, params Object[] obj)
    {

    }

    public void GoodsNumberChange(int level, int number)
    {
     
    }

    public void SpaceNumberChange(int level, int number)
    {
        CreateLevelButton();
    }
    #endregion
}