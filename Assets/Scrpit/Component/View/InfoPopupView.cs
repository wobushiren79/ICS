using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPopupView : BaseMonoBehaviour
{
    //屏幕(用来找到鼠标点击的相对位置)
    public RectTransform screenRTF;

    private void Update()
    {
        if (screenRTF == null)
            return;
        //如果显示Popup 则调整位置为鼠标位置
        if (gameObject.activeSelf)
        {
            Vector2 outPosition;
            //屏幕坐标转换为UI坐标
            RectTransformUtility.ScreenPointToLocalPointInRectangle(screenRTF, Input.mousePosition, Camera.main, out outPosition);
            transform.localPosition = new Vector3(transform.localPosition.x, outPosition.y, transform.localPosition.z);
        }
    }

    /// <summary>
    /// 刷新控件大小
    /// </summary>
    public void RefreshViewSize()
    {
        RectTransform thisRTF = GetComponent<RectTransform>();
        float itemWith = thisRTF.rect.width;
        float itemHight = thisRTF.rect.height;

        RectTransform[] childTFList = GetComponentsInChildren<RectTransform>();
        if (childTFList == null)
            return;
        itemHight = 0;
        foreach (RectTransform itemTF in childTFList)
        {
            itemHight += itemTF.rect.height;
        }
        //设置大小
        if (thisRTF != null)
            thisRTF.sizeDelta = new Vector2(itemWith, itemHight);
    }


}
