using UnityEngine;
using UnityEditor;
using System.Collections;

public class GameAutoSave : BaseMonoBehaviour
{
    public GameDataCpt gameDataCpt;
    public float autoTime = 30;
    public bool isOpenAutoSave = true;

    private void Start()
    {
        autoTime = GameCommonInfo.gameConfig.autoSaveTime;
        StartCoroutine(AutoSave());
    }

    public IEnumerator AutoSave()
    {
        while (isOpenAutoSave)
        {
            yield return new WaitForSeconds(autoTime);
            if (gameDataCpt != null)
                gameDataCpt.SaveUserData();
        }
    }
}