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

    /// <summary>
    /// 分数改变
    /// </summary>
    /// <param name="score"></param>
    void ScoreChange(double score);

    /// <summary>
    /// 等级改变
    /// </summary>
    /// <param name="level"></param>
    void LevelChange(int level);

}