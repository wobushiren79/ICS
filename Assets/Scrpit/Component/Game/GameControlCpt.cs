using UnityEngine;
using UnityEditor;

public class GameControlCpt : BaseMonoBehaviour
{
    public GameCameraCpt gameCameraCpt;
    //右键起始点
    private Vector3 mVecStart;
    //镜头是否移动中
    private bool mIsMove = false;

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            OnMouseUp();
        }
        if (Input.GetMouseButtonDown(1))
        {
            OnMouseDown();
        }
        if (mIsMove)
        {
            OnMouseDrag();
        }
    }

    /// <summary>
    /// 当鼠标按下时触发(其实就是初始化_vec3Offset值，需要注意的是一切的位置坐标都是为了得到这个差值)
    /// </summary>
    private void OnMouseDown()
    {
        mIsMove = true;
        //获取鼠标相对于摄像头的点击位置
        mVecStart = Camera.main.ScreenToWorldPoint(Input.mousePosition+new Vector3(0,0,5));
    }

    /// <summary>
    /// 鼠标抬起时
    /// </summary>
    private void OnMouseUp()
    {
        mIsMove = false;
    }

    /// <summary>
    /// 在用户拖拽GUI元素或碰撞提的时候调用，在鼠标按下的每一帧被调用
    /// </summary>
    private void OnMouseDrag()
    {
        if (mVecStart == null)
            return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 5));
        Vector3 moveOffset = mVecStart - mousePos;
        gameCameraCpt.MoveCamera(new Vector3(moveOffset.x,0));

    }
}