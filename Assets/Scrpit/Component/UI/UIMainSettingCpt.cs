using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIMainSettingCpt : BaseUIComponent
{
    //标题文字
    public Text tvTitle;
    //返回按钮文字
    public Text tvBack;
    //返回按钮
    public Button btBack;

    public Text tvTitleLanguage;
    public Text tvChiliPS;
    public Text tvTitleSound;
    public Text tvTitleAutoSave;

    public Dropdown ddLanguage;
    public Dropdown ddChiliPS;
    public Slider sliderSound;
    public InputField inputAutoSave;

    public GameToastCpt gameToastCpt;

    private void Start()
    {
        if (btBack != null)
            btBack.onClick.AddListener(BTBackOnClick);
        if (ddLanguage != null)
            ddLanguage.onValueChanged.AddListener(LanguageChange);
        if (ddChiliPS != null)
            ddChiliPS.onValueChanged.AddListener(ChiliPSChange);
        InitData();
    }

    public override void OpenUI()
    {
        base.OpenUI();
        InitData();

    }

    public void InitData()
    {
        if (tvTitle != null)
            tvTitle.text = GameCommonInfo.GetTextById(68);
        if (tvBack != null)
            tvBack.text = GameCommonInfo.GetTextById(73);
        if (tvTitleLanguage != null)
            tvTitleLanguage.text = GameCommonInfo.GetTextById(69);
        if (tvChiliPS != null)
            tvChiliPS.text = GameCommonInfo.GetTextById(92);
        if (tvTitleSound != null)
            tvTitleSound.text = GameCommonInfo.GetTextById(70);
        if (tvTitleAutoSave != null)
            tvTitleAutoSave.text = GameCommonInfo.GetTextById(71);
        if (sliderSound != null)
            sliderSound.value = GameCommonInfo.gameConfig.soundVolume;
        if (inputAutoSave != null)
            inputAutoSave.text = GameCommonInfo.gameConfig.autoSaveTime+"";
        if (ddChiliPS != null)
        {
            ddChiliPS.ClearOptions();

            List<Dropdown.OptionData> listChiliPSOptions = new List<Dropdown.OptionData>();
            Dropdown.OptionData itemClose= new Dropdown.OptionData();
            itemClose.text = GameCommonInfo.GetTextById(94);
            listChiliPSOptions.Add(itemClose);
            Dropdown.OptionData itemOpen = new Dropdown.OptionData();
            itemOpen.text = GameCommonInfo.GetTextById(93);
            listChiliPSOptions.Add(itemOpen);
            ddChiliPS.AddOptions(listChiliPSOptions);

            ddChiliPS.value= GameCommonInfo.gameConfig.chiliPS;
        }
 
    }

    public void LanguageChange(int position)
    {

    }

    public void ChiliPSChange(int position)
    {

    }

    /// <summary>
    /// 返回按钮
    /// </summary>
    public void BTBackOnClick()
    {
        if(CheckUtil.StringIsNull(inputAutoSave.text))
        {
            inputAutoSave.text = 30+"";
        }
        if (int.Parse(inputAutoSave.text) <10)
        {
            if (gameToastCpt != null)
                gameToastCpt.ToastHint(GameCommonInfo.GetTextById(72));
            return;
        }

        GameCommonInfo.gameConfig.language = "cn";
        GameCommonInfo.gameConfig.soundVolume = sliderSound.value;
        GameCommonInfo.gameConfig.autoSaveTime = int.Parse(inputAutoSave.text);
        GameCommonInfo.gameConfig.chiliPS = ddChiliPS.value;
        GameCommonInfo.SaveGameConfig();
        uiManager.OpenUIAndCloseOtherByName("Start");
        if (gameToastCpt != null)
            gameToastCpt.ToastHint(GameCommonInfo.GetTextById(74));
    }
}