using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
public class GameBufferItem : PopupReplyView
{

    public Button btItem;

    private void Start()
    {
        if (btItem != null)
            btItem.onClick.AddListener(Test);
    }

    public void Test()
    {
        LogUtil.Log("Test");
    }
    public override void ClosePopup()
    {
       
    }

    public override void OpenPopup()
    {
        infoPopupView.SetInfoData(null,"test",null,null,null,null);
    }
}