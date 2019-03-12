using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

public class GameTimeCpt : BaseMonoBehaviour
{

    public GameDataCpt gameDataCpt;
    public bool isOpenGameTime = true;


    public void Start()
    {
        StartCoroutine(StartTime());
    }

    private void Update()
    {

    }

    public IEnumerator StartTime()
    {
        while (isOpenGameTime)
        {
            yield return new WaitForSeconds(1);
            //游戏时间+1秒
            if (gameDataCpt.userData.gameTime == null)
                gameDataCpt.userData.gameTime = new TimeBean();
            gameDataCpt.userData.gameTime.AddSecond(1);
            //辣椒油
            gameDataCpt.userData.chiliOil += (1f / 3600f);
        }
    }

    private void OnDestroy()
    {
        isOpenGameTime = false;
    }
}