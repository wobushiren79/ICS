using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItemGoodsCpt : BaseMonoBehaviour
{
    private Animator goodsAnimator;

    public void SetLevelData(int level)
    {
        goodsAnimator = GetComponentInChildren<Animator>();
        if (goodsAnimator)
        {
            goodsAnimator.SetInteger("PlayState",1);
            goodsAnimator.SetInteger("Level", level);
        }
    }
}
