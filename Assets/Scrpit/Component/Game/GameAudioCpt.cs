using UnityEngine;
using UnityEditor;

public class GameAudioCpt : AudioView
{
    public  Camera camera;
    public void PlayGameClip(string clipName)
    {
        PlayClip(clipName, camera.transform.position,GameCommonInfo.gameConfig.soundVolume);
    }
}