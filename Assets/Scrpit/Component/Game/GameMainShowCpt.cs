using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class GameMainShowCpt : BaseMonoBehaviour,IGameDataCallBack
{
    //增加按钮
    public Button btAdd;
    //增加的特效模板
    public GameObject itemAddModel;
    //列表样式
    public List<Sprite> listSp;
    //显示的分数
    public Text tvScore;
    //游戏数据控制
    public GameDataCpt dataCpt;

    //屏幕(用来找到鼠标点击的相对位置)
    public RectTransform screenRTF;

    public float addAnimTime =3;

    private void Start()
    {
        dataCpt.AddObserver(this);
        if (btAdd != null)
            btAdd.onClick.AddListener(BTAddOnClick);
    }

    private void OnDestroy()
    {
        if (dataCpt != null)
            dataCpt.RemoveObserver(this);
    }
    /// <summary>
    /// 增加按钮点击
    /// </summary>
    public void BTAddOnClick()
    {
        if (tvScore == null)
            return;
        GameObject addItem = Instantiate(itemAddModel, itemAddModel.transform);
        addItem.SetActive(true);
        addItem.transform.SetParent(transform);
        Vector2 outPosition;
        //屏幕坐标转换为UI坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(screenRTF, Input.mousePosition, Camera.main, out outPosition);
        addItem.transform.localPosition = new Vector3(outPosition.x, outPosition.y, addItem.transform.position.z);
        Vector3 moveLocation = new Vector3(tvScore.transform.position.x, tvScore.transform.position.y+100, tvScore.transform.position.z);
        addItem.transform.DOLocalMove(moveLocation, addAnimTime ).SetEase(Ease.InOutBack).OnComplete(delegate ()
        {
            if (dataCpt != null)
                dataCpt.AddScore(1);
            Destroy(addItem);
        }) ;
        CanvasGroup itemCG = addItem.GetComponent<CanvasGroup>();
        itemCG.DOFade(0, addAnimTime * 0.3f).SetDelay(addAnimTime * 0.7f);
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

    public void LevelChange(int level)
    {

    }

    public void ObserbableUpdate(int type, params Object[] obj)
    {
    
    }


}