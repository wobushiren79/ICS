using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIGameSkillCpt : BaseUIComponent, IGameDataCallBack
{
    //返回按钮
    public Button btBack;
    public Text tvBack;

    //标题
    public Text tvTitle;
    //分数
    public Text tvScore;

    public GameDataCpt gameDataCpt;

    public RectTransform listSkillsRTF;
    public GameObject listSkillsContent;
    public GameObject itemSkillsModel;

    private bool mNeedRefresh=false;

    private void Start()
    {
        gameDataCpt.AddObserver(this);

        if (btBack != null)
            btBack.onClick.AddListener(BTBack);
        if (tvBack != null)
            tvBack.text = GameCommonInfo.GetTextById(36);
        if (tvTitle != null)
            tvTitle.text = GameCommonInfo.GetTextById(33);
        StartCoroutine(RefreshData());
    }

    /// <summary>
    /// 数据更新
    /// </summary>
    private void Update()
    {
        if (gameDataCpt == null)
            return;
        if (tvScore != null)
        {
            string outNumberStr;
            UnitUtil.UnitEnum outUnit;
            UnitUtil.DoubleToStrUnit(gameDataCpt.userData.userScore, out outNumberStr, out outUnit);
            tvScore.text = outNumberStr + " " + GameCommonInfo.GetUnitStr(outUnit);
        }
    }

    /// <summary>
    /// 返回按钮点击
    /// </summary>
    public void BTBack()
    {
        uiManager.OpenUIAndCloseOtherByName("GameMenu");
    }

    public override void OpenUI()
    {
        base.OpenUI();
        if (mNeedRefresh)
        {
            StartCoroutine(RefreshData());
            mNeedRefresh = false;
        }
    }

    /// <summary>
    /// 刷新数据
    /// </summary>
    public IEnumerator RefreshData()
    {
        List<LevelScenesBean> listScenesData = gameDataCpt.GetScenesListByLevel(gameDataCpt.userData.goodsLevel);
        if (CheckUtil.ListIsNull(listScenesData) || listSkillsContent == null || itemSkillsModel == null) {
            yield return 0;
        }
        else
        {
            CptUtil.RemoveChildsByActive(listSkillsContent.transform);

            //增加手指点击数据
            for (int i = 0; i < listScenesData.Count; i++)
            {
                LevelScenesBean itemData = listScenesData[i];
                GameObject itemObj = Instantiate(itemSkillsModel, itemSkillsModel.transform);
                itemObj.transform.SetParent(listSkillsContent.transform);
                itemObj.SetActive(true);
                GameSkillsItem skillItem = itemObj.GetComponent<GameSkillsItem>();
                skillItem.SetData(itemData);
                yield return 0;
            }
            GameUtil.RefreshRectViewHight(listSkillsRTF, true);
        }
    }

    #region
    public void GoodsNumberChange(int level, int number, int totalNumber)
    {
    }

    public void SpaceNumberChange(int level, int number, int totalNumber)
    {
    }

    public void ScoreChange(double score)
    {
    }

    public void ScoreLevelChange(int level)
    {

    }

    public void GoodsLevelChange(int level)
    {
        if (gameObject.activeSelf)
            StartCoroutine(RefreshData());
        else
            mNeedRefresh = true;
    }

    public void ObserbableUpdate(int type, params UnityEngine.Object[] obj)
    {

    }


    #endregion

}
