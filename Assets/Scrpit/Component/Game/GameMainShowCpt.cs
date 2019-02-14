using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class GameMainShowCpt : BaseMonoBehaviour,IGameDataCallBack
{
    //增加按钮
    public Button btAdd;
    public Image ivLight1;
    public Image ivLight2;
    //增加的特效模板
    public GameObject itemAddModel;
    public GameObject itemNumberModel;
    //显示的分数
    public GameObject objBase;
    public Text tvScore;
    //游戏数据控制
    public GameDataCpt gameDataCpt;

    //屏幕(用来找到鼠标点击的相对位置)
    public RectTransform screenRTF;

    public float addAnimTime =3;

    private void Awake()
    {
        if(ivLight1!=null& ivLight2 != null)
        {
            ivLight1.transform.DOLocalRotate(new Vector3(0, 0, 180f),20).SetLoops(-1,LoopType.Yoyo);
            ivLight1.transform.DOScale(new Vector3(0.9f, 0.9f), 10).SetLoops(-1, LoopType.Yoyo);
            ivLight2.transform.DOLocalRotate(new Vector3(0, 0, -180), 13).SetLoops(-1, LoopType.Yoyo);
        }
           
    }

    private void Start()
    {
        GoodsLevelChange(gameDataCpt.userData.goodsLevel);
        gameDataCpt.AddObserver(this);
        if (btAdd != null)
            btAdd.onClick.AddListener(BTAddOnClick);
    }

    private void OnDestroy()
    {
        if (gameDataCpt != null)
            gameDataCpt.RemoveObserver(this);
    }
    /// <summary>
    /// 增加按钮点击
    /// </summary>
    public void BTAddOnClick()
    {
        if (tvScore == null)
            return;
        Vector2 outPosition;

        //成就记录
        gameDataCpt.userData.userAchievement.clickTime++;
        //屏幕坐标转换为UI坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(screenRTF, Input.mousePosition, Camera.main, out outPosition);
        double addScore = 1;
        UserItemLevelBean itemData = gameDataCpt.userData.clickData;
        if (itemData != null)
        {
            addScore = itemData.itemGrow * itemData.itemTimes * itemData.goodsNumber;
        }
        //辣酱动画
        GameObject addItem = Instantiate(itemAddModel, itemAddModel.transform);
        addItem.SetActive(true);
        addItem.transform.SetParent(transform);
        Image sauceIV= addItem.GetComponentInChildren<Image>();
        GameDataCpt.IconKV tempKV = RandomUtil.GetRandomDataByList(gameDataCpt.listSauceData);
        sauceIV.sprite = tempKV.value;

        addItem.transform.localPosition = new Vector3(outPosition.x, outPosition.y, addItem.transform.position.z);
        addItem.transform.DOLocalMove(objBase.transform.localPosition, addAnimTime ).SetEase(Ease.InOutBack).OnComplete(delegate ()
        {
            if (gameDataCpt != null)
            {
                gameDataCpt.AddScore(addScore);
            }          
            Destroy(addItem);
        });
        CanvasGroup itemAddCG = addItem.GetComponent<CanvasGroup>();
        itemAddCG.DOFade(0, addAnimTime * 0.3f).SetDelay(addAnimTime * 0.7f);

        //数量动画
        GameObject numberItem = Instantiate(itemNumberModel, itemNumberModel.transform);
        numberItem.SetActive(true);
        numberItem.transform.SetParent(transform);
        numberItem.transform.localPosition = new Vector3(outPosition.x+10, outPosition.y-10, addItem.transform.position.z);
        numberItem.transform.DOLocalMoveY(numberItem.transform.localPosition.y+100, addAnimTime/2).OnComplete(delegate ()
        {
            Destroy(numberItem);
        });
        CanvasGroup itemNumberCG = numberItem.GetComponent<CanvasGroup>();
        itemNumberCG.DOFade(0, addAnimTime/2 );
        numberItem.GetComponent<Text>().text ="+"+GameCommonInfo.GetPriceStr(addScore);
    }

    public void GoodsNumberChange(int level, int number)
    {
   
    }

    public void SpaceNumberChange(int level, int number)
    {
   
    }

    public void ScoreChange(double score)
    {
        tvScore.transform.DOKill();
        tvScore.transform.localScale = new Vector3(1,1,1);
        tvScore.transform.DOPunchScale(new Vector3(0.5f,0.5f,1),3);
    }

    public void ScoreLevelChange(int level)
    {
      
    }

    public void GoodsLevelChange(int level)
    {
        if (ivLight1 != null && ivLight2 != null) {
            ivLight1.color = new Color(1f, 0f, 0f, 0.2f + (level / 10f));
            ivLight2.color = new Color(1f, 0f, 0f, 0.2f + (level / 10f));
        }
    }

    public void ObserbableUpdate(int type, params Object[] obj)
    {
    
    }


}