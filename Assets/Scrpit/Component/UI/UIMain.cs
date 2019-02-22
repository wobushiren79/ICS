using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UIMain : BaseUIManager
{
    public Button btMaker;

    private void Start()
    {
        if (btMaker != null)
            btMaker.onClick.AddListener(OpenMaker);
    }

    public void OpenMaker()
    {
        OpenUIAndCloseOtherByName("Maker");
    }

}