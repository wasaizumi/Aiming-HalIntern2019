using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClearUI :MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_timeText;
    [SerializeField]
    private TextMeshProUGUI m_bestTimeText;

    [Header("Achievement")]
    [SerializeField]
    public AchivementUI m_achievement_1;
    [SerializeField]
    public AchivementUI m_achievement_2;
    [SerializeField]
    public AchivementUI m_achievement_3;

    [SerializeField]
    public AchievementsUI m_ahievementsUI;

    public void SetTimeText(float time)
    {
        m_timeText.text = TimeText(time);
    }

    public void SetBestTimeText(float time)
    {
        m_bestTimeText.text = TimeText(time);
    }

    private string TimeText(float time)
    {
        return string.Format("{0:00}:{1:00}", Mathf.FloorToInt(time / 60), Mathf.FloorToInt(time % 60));
    }
}
