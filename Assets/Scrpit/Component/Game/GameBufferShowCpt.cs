using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

public class GameBufferShowCpt : BaseMonoBehaviour, IBufferInfoView
{
    public GameBufferListCpt bufferListCpt;

    public RectTransform listShowContent;
    public GameObject itemShowModel;


    public List<BufferInfoBean> listBufferInfoData;
    private BufferInfoController mBufferInfoController;

    public float waitMinTime = 1f;
    public float waitMaxTime = 2f;
    public float animDuration = 20;
    public bool IsShow = true;
    private void Awake()
    {
        mBufferInfoController = new BufferInfoController(this, this);
    }

    private void Start()
    {
        mBufferInfoController.GetAllBufferInfo();
        StartCoroutine(StartShow());
    }

    private IEnumerator StartShow()
    {
        while (IsShow)
        {
            float waitTime = Random.Range(waitMinTime, waitMaxTime);
            yield return new WaitForSeconds(waitTime);
            if (CheckUtil.ListIsNull(listBufferInfoData))
                continue;
            BufferInfoBean itemBufferData = RandomUtil.GetRandomDataByList(listBufferInfoData);
            CreateShowItem(itemBufferData);
        }
    }

    /// <summary>
    /// 创建一个展示
    /// </summary>
    /// <param name="itemBufferData"></param>
    public void CreateShowItem(BufferInfoBean itemBufferData)
    {
        if (itemShowModel == null || listShowContent == null)
            return;
        GameObject showObj = Instantiate(itemShowModel, itemShowModel.transform);
        showObj.transform.SetParent(listShowContent.transform);
        showObj.SetActive(true);
        RectTransform showItemRT= showObj.GetComponent<RectTransform>();
        CanvasGroup showItemCG= showObj.GetComponent<CanvasGroup>();
        Vector2 showPosition = new Vector2(Random.Range(showItemRT.rect.width / 2f, listShowContent.rect.width- showItemRT.rect.width/2f), Random.Range(-(listShowContent.rect.height - showItemRT.rect.height/2f), -showItemRT.rect.height/2f));
        showItemRT.anchoredPosition = showPosition;
        Button btShow = showObj.GetComponent<Button>();
        if (btShow != null)
        {
            btShow.onClick.AddListener(delegate ()
            {
                bufferListCpt.AddBuffer(itemBufferData);
                showObj.transform.DOKill();
                if (showObj)
                    Destroy(showObj);
            });
        }
      
        //设置本身动画
        showObj.transform.localScale=new Vector3(0,0,0);
        showObj.transform.DOScale(new Vector3(1,1,1),1).OnComplete(delegate() {
            showObj.transform.DOScale(new Vector3(0,0), animDuration/2f).SetDelay(animDuration/2f);
        });
        showObj.transform.DOPunchRotation(new Vector3(0, 0, 10),1,5,1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        showItemCG.DOFade(0, animDuration/2f).SetDelay(animDuration / 2f).OnComplete(delegate () {
            showObj.transform.DOKill();
            if (showObj)
                Destroy(showObj);
        });
        Image ivBackground=  CptUtil.GetCptInChildrenByName<Image>(showObj,"Background");
   
    }

    #region 数据回调
    public void GetAllBufferInfoFail()
    {

    }

    public void GetAllBufferInfoSuccess(List<BufferInfoBean> listData)
    {
        listBufferInfoData = listData;
    }
    #endregion
}