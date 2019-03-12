using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Collections;

public class UIRebirthConversionCpt : BaseUIComponent
{
    public Transform tfSauce;
    public CanvasGroup cgSauce;
    public Text tvSauce;

    public Transform tfChili;
    public Text tvChili;

    public ParticleSystem psChange;

    public GameDataCpt gameDataCpt;
    public float changeTime=10;

    private int rebirthChiliNumber = 0;
    private void Start()
    {
        StartChange();
    }

    /// <summary>
    /// 开始转换
    /// </summary>
    public void StartChange()
    {
        if (gameDataCpt == null)
            return;


        //设置数据
        StartCoroutine(CountDownScore(gameDataCpt.userData.userScore, changeTime));
        StartCoroutine(AddChili(gameDataCpt.userData.userScore, changeTime));

        psChange.Play();
        cgSauce.DOFade(0, changeTime / 2f).SetDelay(changeTime / 1.2f);
        tfSauce.DOScale(new Vector3(0.5f, 0.5f, 0), 10).OnComplete(delegate () {
            psChange.Stop();
        });

        tfChili.localScale = new Vector3(0.2f, 0.2f);
        tfChili.DOScale(new Vector3(1,1,1), changeTime).SetDelay(changeTime/10f).OnComplete(delegate() {
            StartCoroutine(OpenTalentUI());
        });
    }

    public IEnumerator OpenTalentUI()
    {
         yield return new WaitForSeconds(2f);

          uiManager.OpenUIAndCloseOtherByName("Talent");
          UIRebirthTalentCpt talentCpt= (UIRebirthTalentCpt)uiManager.GetUIByName("Talent");
          talentCpt.AddRebirthChili(rebirthChiliNumber);
    }

    public IEnumerator CountDownScore(double score,float time)
    {
        while (time>=0)
        {
            yield return new WaitForSeconds(0.1f);
            score -= (score / time)/10f;
            tvSauce.text = GameCommonInfo.GetPriceStr(score);
            time -= 0.1f;
        }
        tvSauce.text ="0";
    }

    public IEnumerator AddChili(double score, float time)
    {
        rebirthChiliNumber = GetChiliNumber(score);
        float itemNumber = (float)rebirthChiliNumber / time/10f;
        float tempNumber = 0;
        while (time >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            tempNumber += itemNumber;
            tvChili.text =(int) tempNumber+"";
            time -= 0.1f;
        }
        tvChili.text = rebirthChiliNumber + "";
    }

    private int GetChiliNumber(double score)
    {
        if (score - 1e8 < 0)
        {
            return 0;
        }
        score = score / 1e8;
        double number= 5 +  Math.Log(score, 2);
        return (int)number;
    }
}