using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PopupReplyView : BaseMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //弹出的窗口
    public InfoPopupView infoPopupView;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (infoPopupView != null)
            infoPopupView.gameObject.SetActive(true);
        OpenPopup();
        infoPopupView.RefreshViewSize();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (infoPopupView != null)
            infoPopupView.gameObject.SetActive(false);
        ClosePopup();
    }

    public abstract void OpenPopup();
    public abstract void ClosePopup();
}
