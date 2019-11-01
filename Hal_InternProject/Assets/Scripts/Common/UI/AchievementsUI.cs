using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementsUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_achievement1;
    [SerializeField]
    private TextMeshProUGUI m_achievement2;
    [SerializeField]
    private TextMeshProUGUI m_achievement3;

    [SerializeField]
    public List<RectTransform> m_selectList;

    public void SetAhievement(GameData data)
    {
        SetAchievement1Text(data.AchieveCoinNum);
        SetAchievement2Text(data.AchieveLimitTime);
        SetAchievement3Text(data.AchieveChangeNum);
    }

    public void SetAchievement1Text(int coinNum)
    {
        m_achievement1.text = string.Format("コインを{0}枚取得する。",coinNum);
    }

    public void SetAchievement2Text(float time)
    {
        m_achievement2.text = string.Format("{0:0}:{1:00}以内にゴールする。",Mathf.FloorToInt(time /60),Mathf.FloorToInt(time %60));
    }

    public void SetAchievement3Text(int changeNum)
    {
        m_achievement3.text = string.Format("磁極の交換を{0}回以内にする。",changeNum);
    }

    
}
