using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UIMainMakerCpt : BaseUIComponent
{
    public Button btBack;
    public Text tvBack;

    private void Start()
    {
        if (btBack != null)
            btBack.onClick.AddListener(OnClickBack);
        if (tvBack != null)
            tvBack.text = GameCommonInfo.GetTextById(4);
    }

    public void OnClickBack()
    {
        uiManager.OpenUIAndCloseOtherByName("Start");
    }
}