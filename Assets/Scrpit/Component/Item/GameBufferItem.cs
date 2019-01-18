using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
public class GameBufferItem : PopupReplyView
{

    public Button btItem;
    public Image ivMask;

    public float amount = 0.5f;
    private void Start()
    {
        if (btItem != null)
            btItem.onClick.AddListener(Test);
       
    }

    private void Update()
    {
        ivMask.fillAmount = amount;
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