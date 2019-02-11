using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GameAchievementSkillCpt : BaseMonoBehaviour
{

    public Text tvHeading;

    private void Start()
    {
        if (tvHeading)
            tvHeading.text = GameCommonInfo.GetTextById(62);
    }

    public void RefreshData()
    {
    }

}