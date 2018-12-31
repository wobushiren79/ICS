using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class GameMainShowCpt : BaseMonoBehaviour
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

    public float addAnimTime = 5;

    private void Start()
    {
        if (btAdd != null)
            btAdd.onClick.AddListener(BTAddOnClick);
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
        addItem.transform.parent = transform;
        Vector2 outPosition;
        //屏幕坐标转换为UI坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(screenRTF, Input.mousePosition, Camera.main, out outPosition);
        addItem.transform.localPosition = new Vector3(outPosition.x, outPosition.y, addItem.transform.position.z);
        addItem.transform.DOMove(tvScore.transform.position, addAnimTime).SetEase(Ease.InOutBack).OnComplete(delegate ()
        {
            if (dataCpt != null)
                dataCpt.AddScore(1);
            Destroy(addItem);
        });
        CanvasGroup itemCG = addItem.GetComponent<CanvasGroup>();
        itemCG.DOFade(0, addAnimTime * 0.3f).SetDelay(addAnimTime * 0.7f);
    }
}