using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopupReplyView : BaseMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //弹出的窗口
    public GameObject popupWindow;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (popupWindow != null)
            popupWindow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (popupWindow != null)
            popupWindow.SetActive(false);
    }
}
