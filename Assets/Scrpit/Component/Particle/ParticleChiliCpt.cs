﻿using UnityEngine;
using UnityEditor;

public class ParticleChiliCpt : BaseMonoBehaviour, IGameDataCallBack
{
    public GameDataCpt gameData;
    private ParticleSystem mChiliParticle;

    private void Awake()
    {
        if (gameData != null)
            gameData.AddObserver(this);
    }

    private void OnDestroy()
    {
        if (gameData != null)
            gameData.RemoveObserver(this);
    }

    private void Start()
    {
        mChiliParticle = GetComponent<ParticleSystem>();
        SetChiliDensity(1);
    }

    /// <summary>
    /// 设置辣椒密度
    /// </summary>
    /// <param name="density"></param>
    public void SetChiliDensity(float density)
    {
        if (mChiliParticle == null)
            return;
        ParticleSystem.EmissionModule emissionModule = mChiliParticle.emission;
        emissionModule.rateOverTime = density;
    }

    #region 用户数据改变回调
    public void GoodsNumberChange(int level, int number)
    {
        SetChiliDensity(gameData.userData.userLevel);
    }

    public void SpaceNumberChange(int level, int number)
    {
        SetChiliDensity(gameData.userData.userLevel);
    }

    public void ScoreChange(double score)
    {

    }

    public void ObserbableUpdate(int type, params Object[] obj)
    {

    }
    #endregion
}