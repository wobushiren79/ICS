using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

public class UIGameNewsInfoCpt : BaseUIComponent,IGameDataCallBack,INewsInfoView
{
    public Text tvContent;
    public CanvasGroup tvCG;
    public Transform tfContent;

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
        while (isShowNews&& gameObject!=null&&gameObject.activeSelf&& tfContent!=null&& tvCG!=null)
        {
            if (!CheckUtil.ListIsNull(listNewsInfoData))
            {
                tvCG.DOFade(0, 1).OnComplete(delegate() {
                    int randomPosition = Random.Range(0, listNewsInfoData.Count);
                    NewsInfoBean itemData = listNewsInfoData[randomPosition];
                    string contentStr = "";
                    if (!CheckUtil.StringIsNull(itemData.content))
                    {
                        contentStr += itemData.content;
                    }
                    if (!CheckUtil.StringIsNull(itemData.author))
                    {
                        contentStr += "\n                        --" + itemData.author;
                    }
                    tvContent.text = contentStr;
                });
                tvCG.DOFade(1,1).SetDelay(1);      
            }
            yield return new WaitForSeconds(newsUpdateTime);
        }
    }

    public void CheckData()
    {
        mNewInfoController.GetNewsInfoByLevel(0, gameDataCpt.userData.goodsLevel);
    }
     
    #region 游戏数据回调
    public void GoodsNumberChange(int level, int number, int totalNumber)
    {

    }

    public void SpaceNumberChange(int level, int number, int totalNumber)
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