using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using System.Threading;

public class GameBufferItem : PopupReplyView
{

    public Button btItem;
    public Image ivMask;
    public Image ivIcon;

    public GameDataCpt gameDataCpt;
    public BufferInfoBean bufferData;
    public LevelScenesBean scenesData;

    public float amount = 1f;
    public float countDownTime = 0;
    public int addTime = 0;

    Thread thread;

    private RebirthTalentItemBean mTalentAddTimeData;
    private void Start()
    {

    }

    private void Update()
    {
        ivMask.fillAmount = amount;
        if (thread != null && !thread.IsAlive)
        {
            transform.DOScale(new Vector3(0, 0), 0.5f).OnComplete(delegate ()
            {
                Destroy(this.gameObject);
            });
        }
    }

    public void SetData(BufferInfoBean bufferData)
    {
        this.bufferData = bufferData;
        scenesData = gameDataCpt.GetScenesByLevel(bufferData.level);
        if (ivIcon != null)
            ivIcon.sprite = gameDataCpt.GetIconByKey(bufferData.icon_key);
        transform.localScale = new Vector3(0, 0, 0);
        transform.DOScale(new Vector3(1, 1), 0.5f);
        if (bufferData == null)
            return;
        //获取天赋-效果持续时间
        mTalentAddTimeData = gameDataCpt.GetRebirthTalentById(404);
        if (mTalentAddTimeData != null)
            addTime += (int)mTalentAddTimeData.total_add;

        thread = new Thread(new ThreadStart(StartTime));
        thread.Start();
    }

    private void StartTime()
    {
        countDownTime = bufferData.time+ addTime;
        while (countDownTime > 0)
        {
            amount = countDownTime /(float)(bufferData.time + addTime);
            Thread.Sleep(1000);
            countDownTime -= 1f;
            double addScore = 0;
            if (bufferData.level == -1)
            {
                addScore = gameDataCpt.userData.userGrow * gameDataCpt.userData.userTimes * bufferData.add_grow;
            }
            else
            {
                UserItemLevelBean userItemLevel = gameDataCpt.GetUserItemLevelDataByLevel(bufferData.level);
                if (userItemLevel != null)
                {
                    addScore = userItemLevel.itemGrow * userItemLevel.itemTimes * userItemLevel.goodsNumber * bufferData.add_grow;
                }
            }
            gameDataCpt.userData.userScore += addScore;
        }
    }



    private void OnDestroy()
    {
        if (thread != null)
        {
            thread.Abort();
        }
        if (infoPopupView != null)
            infoPopupView.gameObject.SetActive(false);
    }

    public override void ClosePopup()
    {

    }

    public override void OpenPopup()
    {
        if (bufferData == null || gameDataCpt == null || scenesData == null)
            return;
        Sprite iconSP = ivIcon.sprite;
        string remark = "❤+ " + bufferData.add_grow * 100 + "%" + scenesData.goods_name + GameCommonInfo.GetTextById(54);
        remark += "\n❤" + GameCommonInfo.GetTextById(95)+ (bufferData.time+addTime)+ "s"; 
        infoPopupView.SetInfoData(iconSP, bufferData.name, "[" + GameCommonInfo.GetTextById(47) + "]", null, bufferData.content, remark);
    }
}