using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraCpt : BaseMonoBehaviour {

    //游戏场景
    public GameScenesCpt gameScenes;
    public Camera gameCamera;
    //镜头移动左边界
    private float mCameraMoveLeftMax;
    //镜头一栋右边界
    private float mCameraMoveRightMax;
    //当前镜头所在等级
    public int cameraLevel;
    private void Start()
    {
        ChangePerspectiveByLevel(1,0);
    }


    /// <summary>
    /// 同级镜头移动
    /// </summary>
    /// <param name="move"></param>
    public void MoveCamera(Vector3 move)
    {
        if (gameCamera == null)
            return;
        float moveAfterX = gameCamera.transform.position.x + move.x;
        if(moveAfterX< mCameraMoveLeftMax||moveAfterX>mCameraMoveRightMax)
        {
            return;
        }
        move = Vector3.Lerp(Vector3.zero, move, 20f * Time.deltaTime);
        gameCamera.transform.Translate(move);
    }

    /// <summary>
    /// 根据等级改变视角位置
    /// </summary>
    /// <param name="level"></param>
    public void ChangePerspectiveByLevel(int level,float x)
    {
        if (gameScenes == null)
            return;
        if (gameCamera == null)
            return;
        
        //设置边界
        gameScenes.GetScenesBorderByLevel(level,out mCameraMoveLeftMax,out mCameraMoveRightMax);
        //获取场景位置
        Vector3 levelScenesPosition= gameScenes.GetScenesPositionByLevel(level);
        //设置摄像头位置
        gameCamera.transform.position = new Vector3(x-2, levelScenesPosition.y+6.5f,-7);
        gameCamera.transform.rotation = new Quaternion();
        gameCamera.transform.Rotate(new Vector3(50,0,0));
        //gameCamera.transform.LookAt(levelScenesPosition);
        cameraLevel = level;
    }

}
