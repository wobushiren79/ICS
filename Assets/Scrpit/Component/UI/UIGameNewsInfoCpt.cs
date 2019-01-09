using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class UIGameNewsInfoCpt : BaseUIComponent,IGameDataCallBack,INewsInfoView
{
    public Text tvContent;

    public List<NewsInfoBean> listNewsInfoData;
    public GameDataCpt gameDataCpt;
    private NewsInfoController mNewInfoController;

    public float newsUpdateTime=10;

    private void Start()
    {
        mNewInfoController = new NewsInfoController(this,this);
        gameDataCpt.AddObserver(this);
        CheckData();
        StartCoroutine(NewsUpdata());
    }

    private void Update()
    {

    }

    public IEnumerator NewsUpdata()
    {
        yield return new WaitForSeconds(newsUpdateTime);
    }

    public void CheckData()
    {
        mNewInfoController.GetNewsInfoByLevel(0, gameDataCpt.userData.userLevel);
    }
     
    #region 游戏数据回调
    public void GoodsNumberChange(int level, int number)
    {

    }

    public void SpaceNumberChange(int level, int number)
    {
       
    }

    public void ScoreChange(double score)
    {

    }

    public void LevelChange(int level)
    {
     
    }

    public void ObserbableUpdate(int type, params Object[] obj)
    {
    }

    public void GetNewsInfoDataSuccess(List<NewsInfoBean> listData)
    {
        this.listNewsInfoData = listData;
    }

    public void GetNewsInfoDataFail()
    {
    }
    #endregion
}