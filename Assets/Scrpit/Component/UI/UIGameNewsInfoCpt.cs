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
    public bool isShowNews = true;

    private void Start()
    {
        mNewInfoController = new NewsInfoController(this,this);
        if (gameDataCpt == null)
            return;
        gameDataCpt.AddObserver(this);
        CheckData();
        StartCoroutine(NewsUpdata());
    }

    private void OnDestroy()
    {
        isShowNews = false;
    }

    private void Update()
    {

    }

    public IEnumerator NewsUpdata()
    {
        while (isShowNews&& gameObject!=null&&gameObject.activeSelf)
        {
            if (!CheckUtil.ListIsNull(listNewsInfoData))
            {
                int randomPosition = Random.Range(0, listNewsInfoData.Count);
                LogUtil.Log("randomPosition" + randomPosition);
                NewsInfoBean itemData= listNewsInfoData[randomPosition];
                tvContent.text = itemData.content;
            }
            yield return new WaitForSeconds(newsUpdateTime);
        }
    }

    public void CheckData()
    {
        mNewInfoController.GetNewsInfoByLevel(0, gameDataCpt.userData.goodsLevel);
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

    public void ScoreLevelChange(int level)
    {
    }

    public void GoodsLevelChange(int level)
    {
        CheckData();
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