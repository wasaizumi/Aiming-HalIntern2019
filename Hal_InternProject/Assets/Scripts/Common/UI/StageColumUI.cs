using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageColumUI : MonoBehaviour
{

    [SerializeField]
    private GameData m_stageSelectData;

    [SerializeField]
    private Sprite lockUI;
    [SerializeField,HideInInspector]
    private Image m_stageImage;
    [SerializeField,HideInInspector]
    private TextMeshProUGUI m_stageName;
    [SerializeField,HideInInspector]
    private TextMeshProUGUI m_stageTime;

    [SerializeField,HideInInspector]
    private Image m_achievement1;
    [SerializeField,HideInInspector]
    private Image m_achievement2;
    [SerializeField,HideInInspector]
    private Image m_achievement3;

    private void OnDrawGizmos()
    {
        this.Update();
    }

    public void SetStageSelectData(GameData selectData)
    {
        this.m_stageSelectData = selectData;
    }

    void Update()
    {
        if (m_stageSelectData.IsLock)
        {
            m_stageImage.sprite = lockUI;
            m_stageName.text = m_stageSelectData.StageName;
            m_stageTime.gameObject.SetActive(false);
            m_achievement1.gameObject.SetActive(false);
            m_achievement2.gameObject.SetActive(false);
            m_achievement3.gameObject.SetActive(false);
        }
        else
        {
            m_stageTime.gameObject.SetActive(true);
            m_achievement1.gameObject.SetActive(true);
            m_achievement2.gameObject.SetActive(true);
            m_achievement3.gameObject.SetActive(true);
            
            m_stageImage.sprite = m_stageSelectData.StageImage;
            m_stageName.text = m_stageSelectData.StageName;

            float time = m_stageSelectData.Time;
            m_stageTime.text = string.Format("{0:00}:{1:00}", (int)time / 60, (int)time % 60);
            


            m_achievement1.color = m_stageSelectData.IsAchievement1 ? Color.white : Color.black;
            m_achievement2.color = m_stageSelectData.IsAchievement2 ? Color.white : Color.black;
            m_achievement3.color = m_stageSelectData.IsAchievement3 ? Color.white : Color.black;
        }
    }
}
