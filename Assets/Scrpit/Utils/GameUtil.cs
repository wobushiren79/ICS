using UnityEngine;
using UnityEditor;

public class GameUtil
{
    /// <summary>
    /// 获取屏幕宽
    /// </summary>
    /// <returns></returns>
    public static float GetScreenWith()
    {
        float leftBorder;
        float rightBorder;
        float topBorder;
        float downBorder;
        Vector3 cornerPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Mathf.Abs(Camera.main.transform.position.z)));

        leftBorder = Camera.main.transform.position.x - (cornerPos.x - Camera.main.transform.position.x);
        rightBorder = cornerPos.x;
        topBorder = cornerPos.y;
        downBorder = Camera.main.transform.position.y - (cornerPos.y - Camera.main.transform.position.y);

        return rightBorder - leftBorder;
    }
    /// <summary>
    /// 获取屏幕高
    /// </summary>
    /// <returns></returns>
    public static float GetScreenHeight()
    {
        float leftBorder;
        float rightBorder;
        float topBorder;
        float downBorder;
        Vector3 cornerPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Mathf.Abs(Camera.main.transform.position.z)));

        leftBorder = Camera.main.transform.position.x - (cornerPos.x - Camera.main.transform.position.x);
        rightBorder = cornerPos.x;
        topBorder = cornerPos.y;
        downBorder = Camera.main.transform.position.y - (cornerPos.y - Camera.main.transform.position.y);

        return topBorder - downBorder;
    }

    /// <summary>
    /// 离开游戏
    /// </summary>
    public static void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
		Application.Quit();
        #endif
    }
}