using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class UIGameLevelInfoCpt : BaseUIComponent,IGameDataCallBack
{
    //游戏数据控制
    public GameDataCpt gameDataCpt;
    //游戏镜头控制
    public GameCameraCpt gameCameraCpt;

    //等级容器
    public GameObject levelContent;
    public RadioGroupView levelRG;
    public GameObject levelItemModel;
    public GameAudioCpt gameAudioCpt;

    public List<Sprite> levelIconList;

    private void Start()
    {
        if (gameDataCpt != null)
            gameDataCpt.AddObserver(this);
        CreateLevelButton();
        StartCoroutine(InitRadioGroup());
    }

    private void OnDestroy()
    {
        if (gameDataCpt != null)
            gameDataCpt.RemoveObserver(this);
    }

    public void CreateLevelButton()
    {
        if (gameDataCpt == null|| levelItemModel == null|| levelContent==null|| levelItemModel == null)
            return;
        //删除原数据
        CptUtil.RemoveChildsByActive(levelContent.transform);

        List<UserItemLevelBean> userLevelDataList =  gameDataCpt.userData.listUserLevelData;
        if (CheckUtil.ListIsNull(userLevelDataList))
            return;
        for(int i=0;i< userLevelDataList.Count; i++)
        {
            UserItemLevelBean levelData= userLevelDataList[i];
            if (levelData.spaceNumber == 0)
                return;
            GameObject levelObj =   Instantiate(levelItemModel, levelItemModel.transform);
            levelObj.SetActive(true);
            levelObj.transform.SetParent(levelContent.transform);
            //设置等级图片
            Image itemImage = CptUtil.GetCptInChildrenByName<Image>(levelObj,"Icon");
            itemImage.sprite = levelIconList[levelData.level-1];
            //设置按钮
            Button itemButton = levelObj.GetComponent<Button>();
            itemButton.onClick.AddListener(delegate() {
                gameCameraCpt.ChangePerspectiveByLevel(levelData.level,0);
                gameAudioCpt.PlayClip("btn_clip_4", Camera.main.transform.position,1);
            });    
        }
    }

    #region 游戏数据回调
    public void ObserbableUpdate(int type, params Object[] obj)
    {

    }

    public void ScoreChange(double score)
    {

    }

    public void ScoreLevelChange(int level)
    {

    }

    public void GoodsLevelChange(int level)
    {

    }

    public void GoodsNumberChange(int level, int number, int totalNumber)
    {

    }

    public void SpaceNumberChange(int level, int number, int totalNumber)
    {
        //创建Item
        CreateLevelButton();
        StartCoroutine(InitRadioGroup());
    }
    #endregion

    private IEnumerator InitRadioGroup()
    {
        yield return new WaitForEndOfFrame();
        //重新初始化RB
        if (levelRG != null)
            levelRG.AutoFindRadioButton();
        RadioButtonView itemRB=  levelRG.listButton[gameCameraCpt.cameraLevel - 1];
        levelRG.RadioButtonSelected(itemRB);
    }

}