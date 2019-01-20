using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;
public class GameBufferListCpt : BaseMonoBehaviour
{
    public GameObject itemBufferModel;
    public GameObject listBufferContent;

 
    /// <summary>
    /// 增加1个BUFFer
    /// </summary>
    /// <param name="bufferData"></param>
    public void AddBuffer(BufferInfoBean bufferData)
    {
        if (bufferData.add_grow == 0 || bufferData.time == 0)
            return;
        if (itemBufferModel == null|| listBufferContent==null)
            return;
        GameObject itemObj =  Instantiate(itemBufferModel,itemBufferModel.transform);
        itemObj.transform.SetParent(listBufferContent.transform);
        itemObj.SetActive(true);
        GameBufferItem itemBuffer= itemObj.GetComponent<GameBufferItem>();
        if (itemBuffer != null)
           itemBuffer.SetData(bufferData);
    }
}