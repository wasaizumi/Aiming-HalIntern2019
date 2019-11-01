using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayMode
{
    SinglePlay,
    MultiPlay
}

[CreateAssetMenu(menuName = "GameData/SystemData",fileName = "CommmonData")]
public class SystemData : ScriptableObject
{
    [SerializeField]
    private PlayMode m_playMode = PlayMode.SinglePlay;
    [SerializeField]
    private int m_nSelectStageNum = 0;

    [Header("StageMenu")]
    [SerializeField,Tooltip("一人プレイ用ステージ群")]
    private StageMenu m_singlePlayStages;
    [SerializeField,Tooltip("二人プレイ用ステージ群")]
    private StageMenu m_coopPlayStages;

    public PlayMode PlayMode
    {
        get { return m_playMode; }
    }

    //
    public int StageSelectNum
    {
        get {
            if (m_nSelectStageNum >= StageMenu.m_stageList.Count)
                m_nSelectStageNum = 0;
            return m_nSelectStageNum; 
        }

        set
        {
            m_nSelectStageNum = value;
            if (m_nSelectStageNum >= StageMenu.m_stageList.Count)
                m_nSelectStageNum = 0;
        }
    }

    public GameData CurrentStage
    {
        get { return StageMenu.m_stageList[m_nSelectStageNum]; }
    }

    //PlayModeを設定
    public void SetPlayMode(PlayMode playMode)
    {
        m_nSelectStageNum = 0;
        m_playMode = playMode;
    }

    public StageMenu StageMenu
    {
        get {
            if (m_playMode == PlayMode.SinglePlay)
                return m_singlePlayStages;
            if (m_playMode == PlayMode.MultiPlay)
                return m_coopPlayStages;
            return m_singlePlayStages;
        }
    }

    public void SetStageData(int index)
    {
        m_nSelectStageNum = index;
    }

    public bool ChangeNextStage()
    {
        m_nSelectStageNum++;
        if(m_nSelectStageNum >= StageMenu.m_stageList.Count)
        {
            m_nSelectStageNum = StageMenu.m_stageList.Count -1;
            //次が無い
            return false;
        }
        return true;
    }

    public void UnLockNestStage()
    {
        if (m_nSelectStageNum + 1 >= StageMenu.m_stageList.Count) return;
        StageMenu.m_stageList[m_nSelectStageNum + 1].UnLock();
    }

    public void Reset()
    {
        m_singlePlayStages.Reset();
        m_coopPlayStages.Reset();
    }

    public void AllUnLock()
    {
        m_singlePlayStages.AllUnLock();
        m_coopPlayStages.AllUnLock();
    }
}
