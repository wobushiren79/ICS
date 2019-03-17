using UnityEngine;
using UnityEditor;
using DG.Tweening;

public class GameTurntableCpt : BaseMonoBehaviour
{
    public GameObject turntableTableObj;
    public CallBack callBack;
    public ParticleSystem saucePS;
    public GameAudioCpt gameAudioCpt;

    public double betSorce;
    public void StartGame(double bet, CallBack callBack)
    {
        this.betSorce = bet;
        ResetTurntable();
        this.callBack = callBack;

        float randomPro = Random.Range(0f, 1f);
        float axisRotate = 0;
        int rewardType = 0;
        if (randomPro <= 0.6f)
        {
            axisRotate = Random.Range(2f, 178f);
            rewardType = 0;
        }
        else if (randomPro > 0.6f && randomPro <= 0.9f)
        {
            axisRotate = Random.Range(182f, 268f);
            rewardType = 1;
        }
        else if (randomPro > 0.9f && randomPro <= 0.99f)
        {
            axisRotate = Random.Range(272f, 313f);
            rewardType = 2;
        }
        else
        {
            axisRotate = Random.Range(317f, 358f);
            rewardType = 3;
        }

        if (turntableTableObj == null)
            return;
        if (gameAudioCpt != null)
            gameAudioCpt.PlayGameClip("bgm_turntable");
        turntableTableObj.transform.DOLocalAxisRotate(new Vector3(0, 0, 11520 + axisRotate), 8).SetEase(Ease.InOutQuart).OnComplete(delegate ()
        {
            RewardDeal(rewardType);
        });
    }

    /// <summary>
    /// 得奖处理
    /// </summary>
    /// <param name="rewardType"></param>
    public void RewardDeal(int rewardType)
    {
        float delay = 0;
        bool isPlayPS = false;
        switch (rewardType)
        {
            case 0:
                betSorce = 0;
                if (gameAudioCpt != null)
                    gameAudioCpt.PlayGameClip("turntable_fail");
                break;
            case 1:
                betSorce = betSorce * 2f;
                delay = 5f;
                isPlayPS = true;
                break;
            case 2:
                betSorce = betSorce * 3f;
                delay = 5f;
                isPlayPS = true;
                break;
            case 3:
                betSorce = betSorce * 5f;
                delay = 5f;
                isPlayPS = true;
                break;
        }
        if (isPlayPS)
        {
            if (gameAudioCpt != null)
                gameAudioCpt.PlayGameClip("bgm_water");
            saucePS.Play();
        }

        transform.DOScale(new Vector3(1, 1, 1), delay).OnComplete(delegate() {
            if (callBack != null)
                callBack.EndGame(betSorce);
        });
    }

    public void ResetTurntable()
    {
        turntableTableObj.transform.DOKill();
        turntableTableObj.transform.localRotation = new Quaternion();
    }

    public interface CallBack
    {
        void EndGame(double reward);
    }
}