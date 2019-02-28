using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GameElementsCpt : BaseMonoBehaviour,IGameDataCallBack
{
    public Image ivBackground;

    public GameDataCpt gameDataCpt;

    private void Start()
    {
        if (gameDataCpt == null)
            return;
        gameDataCpt.AddObserver(this);
        ChangeBackgroundColor(gameDataCpt.userData.goodsLevel);
    }

    public void ChangeBackgroundColor(int level)
    {
        if (ivBackground == null)
            return;
        float colorF =1-((float)level / (float)15)+0.2f;
        ivBackground.color = new Color(1, colorF, colorF);
    }

    #region 数据回调
    public void GoodsLevelChange(int level)
    {
        ChangeBackgroundColor(level);
    }

    public void GoodsNumberChange(int level, int number, int totalNumber)
    {
    }

    public void ObserbableUpdate(int type, params Object[] obj)
    {
    }

    public void ScoreChange(double score)
    {
    }

    public void ScoreLevelChange(int level)
    {
    }

    public void SpaceNumberChange(int level, int number, int totalNumber)
    {
    }
    #endregion

}