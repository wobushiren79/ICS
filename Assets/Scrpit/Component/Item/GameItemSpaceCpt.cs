using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItemSpaceCpt : BaseMonoBehaviour
{

    public int itemSpaceLevel;
    
    /// <summary>
    /// 设置数据
    /// </summary>
    /// <param name="level"></param>
    public void SetLevelData(int level)
    {
        this.itemSpaceLevel = level;
    }

    /// <summary>
    /// 设置纹理
    /// </summary>
    /// <param name="levelTexture"></param>
    public void SetLevelTexture( Texture levelTexture)
    {
        Renderer thisRenderer = GetComponent<Renderer>();
        if (thisRenderer == null || thisRenderer.material==null)
            return;
        if (levelTexture == null)
            return;
        thisRenderer.material.mainTexture= levelTexture;
    }

}
