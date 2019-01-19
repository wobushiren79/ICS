using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class GameBufferItem : PopupReplyView
{

    public Button btItem;
    public Image ivMask;

    public GameDataCpt gameDataCpt;
    public BufferInfoBean bufferData;

    public float amount = 1f;
    public float countDownTime = 0;

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void SetData(BufferInfoBean bufferData)
    {
        transform.localScale = new Vector3(0, 0, 0);
        transform.DOScale(new Vector3(1, 1), 0.5f);
        if (bufferData == null)
            return;
        this.bufferData = bufferData;
        StartCoroutine(StartTime(bufferData));
    }

    private IEnumerator StartTime(BufferInfoBean bufferData)
    {
        countDownTime = bufferData.time;
        while (countDownTime > 0)
        {
            amount = countDownTime / this.bufferData.time;
            ivMask.fillAmount = amount;
            yield return new WaitForSeconds(0.1f);
            countDownTime-=0.1f;
        }
        transform.DOScale(new Vector3(0, 0), 0.5f).OnComplete(delegate() {
            Destroy(this.gameObject);
        });
    }

    private void OnDestroy()
    {
        if(infoPopupView!=null)
        infoPopupView.gameObject.SetActive(false);  
    }

    public override void ClosePopup()
    {
       
    }

    public override void OpenPopup()
    {
        if (bufferData == null|| gameDataCpt==null)
            return;
        Sprite iconSP= gameDataCpt.GetIconByKey(bufferData.icon_key);
        infoPopupView.SetInfoData(iconSP, bufferData.name, null,null, bufferData.content, null);
    }
}