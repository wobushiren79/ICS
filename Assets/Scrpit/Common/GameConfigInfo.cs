using UnityEngine;
using UnityEditor;

public class GameConfigInfo 
{
    public static string LANGUAGE = "cn";
    private static GameConfigController mGameConfigController;
     
    static GameConfigInfo()
    {
        mGameConfigController = new GameConfigController(null, new GameConfigCallBack());
        mGameConfigController.GetGameConfigData();
    }

    public class GameConfigCallBack : IGameConfigView
    {
        public void GetGameConfigFail()
        {

        }

        public void GetGameConfigSuccess(GameConfigBean configBean)
        {
            GameConfigInfo.LANGUAGE = configBean.language;
        }

        public void SetGameConfigFail()
        {

        }

        public void SetGameConfigSuccess(GameConfigBean configBean)
        {

        }
    }
 
}