using UnityEngine;
using UnityEditor;

public class CptUtil
{

    /// <summary>
    /// 删除所有子物体
    /// </summary>
    /// <param name="tf"></param>
    public static void RemoveChild( Transform tf)
    {
        for (int i = 0; i < tf.childCount; i++)
        {
          GameObject.Destroy(tf.GetChild(0).gameObject);
        }
    }

    /// <summary>
    /// 删除所有显示的子物体
    /// </summary>
    /// <param name="tf"></param>
    public static void RemoveChildsByActive( Transform tf)
    {
        for (int i = 0; i < tf.childCount; i++)
        {
            if (tf.GetChild(0).gameObject.activeSelf)
            {
                GameObject.Destroy(tf.GetChild(0).gameObject);
            }
        }
    }
}