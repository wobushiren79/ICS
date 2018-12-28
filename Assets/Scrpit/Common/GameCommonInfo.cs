using UnityEngine;
using UnityEditor;

public class GameCommonInfo 
{
    public static string LANGUAGE = "cn";
    public static string gameUserId;

    private static GameConfigController mGameConfigController;
    private static UITextController mUITextController;
     
    static GameCommonInfo()
    {
        mGameConfigController = new GameConfigController(null, new GameConfigCallBack());
        mUITextController = new UITextController(null,null);
        mGameConfigController.GetGameConfigData();
    }

    public static string GetTextById(long id)
    {
       return mUITextController.GetTextById(id);
    }

    public static string GetUnitStr(UnitUtil.UnitEnum unitEnum)
    {
        string unitStr = "";
        switch (unitEnum)
        {
            case UnitUtil.UnitEnum.Million:
                unitStr =GetTextById(6);
                break;
            case UnitUtil.UnitEnum.Billion:
                unitStr = GetTextById(7);
                break;
            case UnitUtil.UnitEnum.Trillion:
                unitStr = GetTextById(8);
                break;
            case UnitUtil.UnitEnum.Quadrillion:
                unitStr = GetTextById(9);
                break;
            case UnitUtil.UnitEnum.Quintillion:
                unitStr = GetTextById(10);
                break;
            case UnitUtil.UnitEnum.Sextillion:
                unitStr = GetTextById(11);
                break;
            case UnitUtil.UnitEnum.Septillion:
                unitStr = GetTextById(12);
                break;
            case UnitUtil.UnitEnum.Octillion:
                unitStr = GetTextById(13);
                break;
            case UnitUtil.UnitEnum.Nonillion:
                unitStr = GetTextById(14);
                break;
            case UnitUtil.UnitEnum.Decillion:
                unitStr = GetTextById(15);
                break;
            case UnitUtil.UnitEnum.Undecillion:
                unitStr = GetTextById(16);
                break;
            case UnitUtil.UnitEnum.Duodecillion:
                unitStr = GetTextById(17);
                break;
            case UnitUtil.UnitEnum.Tredecillion:
                unitStr = GetTextById(18);
                break;
            case UnitUtil.UnitEnum.Quattuordecillion:
                unitStr = GetTextById(19);
                break;
            case UnitUtil.UnitEnum.Quindecillion:
                unitStr = GetTextById(20);
                break;
            case UnitUtil.UnitEnum.Exdecillion:
                unitStr = GetTextById(21);
                break;
            case UnitUtil.UnitEnum.Septendecillion:
                unitStr = GetTextById(22);
                break;
            case UnitUtil.UnitEnum.Octodecillion:
                unitStr = GetTextById(23);
                break;
            case UnitUtil.UnitEnum.Novemdecillion:
                unitStr = GetTextById(24);
                break;
            case UnitUtil.UnitEnum.Vigintillion:
                unitStr = GetTextById(25);
                break;
            case UnitUtil.UnitEnum.Centillion:
                unitStr = GetTextById(26);
                break;
            default:
                unitStr = "";
               break;

        }
        return unitStr;
    }

    public class GameConfigCallBack : IGameConfigView
    {
        public void GetGameConfigFail()
        {

        }

        public void GetGameConfigSuccess(GameConfigBean configBean)
        {
            GameCommonInfo.LANGUAGE = configBean.language;
            mUITextController.RefreshData();
        }

        public void SetGameConfigFail()
        {

        }

        public void SetGameConfigSuccess(GameConfigBean configBean)
        {

        }
    }
}