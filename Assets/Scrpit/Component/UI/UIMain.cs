using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UIMain : BaseUIManager
{
    public Button btMaker;
    public Text tvTitle;

    private void Start()
    {
        if (btMaker != null)
            btMaker.onClick.AddListener(OpenMaker);
    }


    private void Update()
    {
        if (tvTitle == null)
            return;
        if (GameCommonInfo.gameConfig.language.Equals("cn"))
        {
            tvTitle.text = "无尽的辣酱";
        }
        else
        {
            tvTitle.text = "Infinite Chili Sauce";
        }
    }

    public void OpenMaker()
    {
        OpenUIAndCloseOtherByName("Maker");
    }

}