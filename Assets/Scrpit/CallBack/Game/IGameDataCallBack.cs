using UnityEngine;
using UnityEditor;

public interface IGameDataCallBack : IBaseObserver
{
    /// <summary>
    /// 商品数量改变
    /// </summary>
    /// <param name="level"></param>
    /// <param name="number"></param>
    void GoodsNumberChange(int level,int number);

    /// <summary>
    /// 场地数量改变
    /// </summary>
    /// <param name="level"></param>
    /// <param name="number"></param>
    void SpaceNumberChange(int level,int number);
}