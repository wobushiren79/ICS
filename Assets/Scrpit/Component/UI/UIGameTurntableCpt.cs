using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;
public class UIGameTurntableCpt : BaseUIComponent,GameTurntableCpt.CallBack
{
    //返回按钮
    public Button btBack;
    public Text tvBack;
    //分数
    public Text tvScore;
    //标题
    public Text tvTitle;
    //警告
    public Text tvWarning;
    //赌注
    public Text tvBet;
    public Text tvBetTitle;

    //按钮开始
    public Button btStart;
    public Text tvStart;
    //分数变换
    public GameObject tvChangeScoreOne;
    public Transform tfChangeOneFather;
    public GameObject tvChangeScoreTwo;
    public Transform tfChangeTwoFather;

    public GameDataCpt gameDataCpt;
    public GameToastCpt gameToastCpt;
    public GameTurntableCpt gameTurntableCpt;

    private bool mIsStart = false;
    private double mBetScore = 0;
    private void Start()
    {
        if (btBack != null)
            btBack.onClick.AddListener(BTBackOnClick);
        if (tvBack != null)
            tvBack.text = GameCommonInfo.GetTextById(36);
        if (tvTitle != null)
            tvTitle.text = GameCommonInfo.GetTextById(107);
        if (tvWarning != null)
            tvWarning.text = GameCommonInfo.GetTextById(108);
        if (tvBetTitle != null)
            tvBetTitle.text = GameCommonInfo.GetTextById(110);
        if (tvStart != null)
            tvStart.text = GameCommonInfo.GetTextById(112);
        if (btStart != null)
            btStart.onClick.AddListener(TurntableStart);
    }

    private void Update()
    {
        if (gameDataCpt == null)
            return;
        if (tvScore != null)
            tvScore.text = GameCommonInfo.GetPriceStr(gameDataCpt.userData.userScore);
         
    }

    public void TurntableStart()
    {
        if (mIsStart)
        {
            gameToastCpt.ToastHint(GameCommonInfo.GetTextById(109));
            return;
        }
        mIsStart = true;
        StartPre();
    }

    //开始准备
    public void StartPre()
    {
        gameTurntableCpt.ResetTurntable();
        //移除分数
        mBetScore = gameDataCpt.userData.userScore;
        gameDataCpt.userData.userScore = 0;
        gameDataCpt.SaveUserData();
        //移除分数动画
        GameObject itemRemoveScoreObj=  Instantiate(tvChangeScoreOne, tvChangeScoreOne.transform);
        itemRemoveScoreObj.transform.SetParent(tfChangeOneFather);
        itemRemoveScoreObj.SetActive(true);
        itemRemoveScoreObj.transform.position = tvChangeScoreOne.transform.position;
        Text itemRemoveScoreTV= itemRemoveScoreObj.GetComponent<Text>();
        itemRemoveScoreTV.text = GameCommonInfo.GetPriceStr(mBetScore);
        CanvasGroup itemRemoveScoreCG= itemRemoveScoreObj.GetComponent<CanvasGroup>();
        itemRemoveScoreCG.alpha = 1;
        itemRemoveScoreCG.DOFade(0, 3);
        itemRemoveScoreObj.transform.DOScale(new Vector3(5, 5, 5), 3).OnComplete(delegate ()
        {
            Destroy(itemRemoveScoreObj);
        });
        //添加分数动画
        GameObject itemAddScoreObj = Instantiate(tvChangeScoreTwo, tvChangeScoreTwo.transform);
        itemAddScoreObj.transform.SetParent(tfChangeTwoFather);
        itemAddScoreObj.SetActive(true);
        itemAddScoreObj.transform.position = tvChangeScoreTwo.transform.position;
        itemAddScoreObj.transform.localScale=new Vector3(5,5,5);
        Text itemAddScoreTV = itemAddScoreObj.GetComponent<Text>();
        itemAddScoreTV.text = GameCommonInfo.GetPriceStr(mBetScore);
        itemAddScoreTV.color = new Color(0,1,0);
        CanvasGroup itemAddScoreCG = itemAddScoreObj.GetComponent<CanvasGroup>();
        itemAddScoreCG.alpha = 0;
        itemAddScoreCG.DOFade(1, 3);
        itemAddScoreObj.transform.DOScale(new Vector3(1, 1, 1), 3).OnComplete(delegate ()
        {
            tvBet.text = GameCommonInfo.GetPriceStr(mBetScore);
            Destroy(itemAddScoreObj);
            gameTurntableCpt.StartGame(mBetScore,this);
        });
    }



    /// <summary>
    /// 返回按钮点击
    /// </summary>
    public void BTBackOnClick()
    {
        if (mIsStart)
        {
            gameToastCpt.ToastHint(GameCommonInfo.GetTextById(109));
            return;
        }
        uiManager.OpenUIAndCloseOtherByName("GameMenu");
    }

    public void EndGame(double reward)
    {
        tvBet.text = GameCommonInfo.GetPriceStr(0);
        if (reward == 0)
        {
            //移除分数动画
            GameObject itemRemoveScoreObj = Instantiate(tvChangeScoreTwo, tvChangeScoreTwo.transform);
            itemRemoveScoreObj.transform.SetParent(tfChangeTwoFather);
            itemRemoveScoreObj.SetActive(true);
            itemRemoveScoreObj.transform.position = tvChangeScoreTwo.transform.position;
            Text itemRemoveScoreTV = itemRemoveScoreObj.GetComponent<Text>();
            itemRemoveScoreTV.text = GameCommonInfo.GetPriceStr(mBetScore);
            itemRemoveScoreTV.color = new Color(1, 0, 0);
            CanvasGroup itemRemoveScoreCG = itemRemoveScoreObj.GetComponent<CanvasGroup>();
            itemRemoveScoreCG.alpha = 1;
            itemRemoveScoreCG.DOFade(0, 3);
            itemRemoveScoreObj.transform.DOScale(new Vector3(5, 5, 5), 3).OnComplete(delegate ()
            {
                tvBet.text = GameCommonInfo.GetPriceStr(0);
                Destroy(itemRemoveScoreObj);
            });
            mBetScore = 0;
            mIsStart = false;
            return;
        }
        //移除动画
        mBetScore = reward;
        //添加分数动画
        GameObject itemAddScoreObj = Instantiate(tvChangeScoreOne, tvChangeScoreOne.transform);
        itemAddScoreObj.transform.SetParent(tfChangeOneFather);
        itemAddScoreObj.SetActive(true);
        itemAddScoreObj.transform.position = tvChangeScoreOne.transform.position;
        itemAddScoreObj.transform.localScale = new Vector3(5, 5, 5);
        Text itemAddScoreTV = itemAddScoreObj.GetComponent<Text>();
        itemAddScoreTV.text = GameCommonInfo.GetPriceStr(mBetScore);
        itemAddScoreTV.color = new Color(0, 1, 0);
        CanvasGroup itemAddScoreCG = itemAddScoreObj.GetComponent<CanvasGroup>();
        itemAddScoreCG.alpha = 0;
        itemAddScoreCG.DOFade(1, 3);
        itemAddScoreObj.transform.DOScale(new Vector3(1, 1, 1), 3).OnComplete(delegate ()
        {
            gameDataCpt.userData.userScore += mBetScore;
            mBetScore = 0;
            Destroy(itemAddScoreObj);
            mIsStart = false;
        });

    }
}