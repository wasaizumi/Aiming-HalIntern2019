using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Difficulty
{
    Easy,
    Normal,
    Hard
}

[CreateAssetMenu(menuName = "GameData/GameData", fileName = "GameData")]
public class GameData : ScriptableObject
{
    const float cMaxTime = 60f * 99f + 59f;

    [Header("StagePrefab")]
    [SerializeField, Tooltip("GameSceneで使うステージ")]
    private GameObject m_prefab;

    [Header("StageHeader")]
    [SerializeField, Tooltip("Stageの名前")]
    private string m_stageName = "NONE";
    [SerializeField, Tooltip("Stageのイメージ画像")]
    private Sprite m_stageImage;
    [SerializeField, Tooltip("難易度")]
    private Difficulty m_difficulty = Difficulty.Normal;

    [Header("AchievementParameter")]
    [SerializeField, Tooltip("達成1：コイン数")]
    private int m_achieveCoinNum = 3;
    [SerializeField,Tooltip("達成2：リミットタイム")]
    private float m_limitTime = 60f;
    [SerializeField,Tooltip("達成3：入れ替え数")]
    private int m_changeNum = 3;

    [Header("SaveData")]
    [SerializeField]
    private bool m_isLock;
    [SerializeField]
    private bool m_onceClear = false;
    [SerializeField,NonEditable]
    private float m_time = cMaxTime;

    [SerializeField]
    private bool m_isAchievement1;
    [SerializeField]
    private bool m_isAchievement2;
    [SerializeField]
    private bool m_isAchievement3;

    public GameObject StagePrefab
    {
        get { return m_prefab; }
    }

    public string StageName
    {
        get { return m_stageName; }
    }

    public Sprite StageImage
    {
        get { return m_stageImage; }
    }

    public Difficulty Difficulty
    {
        get { return m_difficulty; }
    }

    public bool IsLock
    {
        get { return m_isLock; }
    }

    public bool IsOnceClear
    {
        get { return m_onceClear; }
        private set { m_onceClear = value; }
    }
    public float Time
    {
        get { return m_time; }
        private set {
            m_time = Mathf.Min(value,m_limitTime);
        }
    }

    public void IsClear()
    {
        m_onceClear = true;
    }

    public void SetTime(float time = 0)
    {
        this.Time = time;
    }

    public int AchieveCoinNum
    {
        get { return m_achieveCoinNum; }
    }

    public float AchieveLimitTime
    {
        get { return m_limitTime; }
    }
    public int AchieveChangeNum
    {
        get { return m_changeNum; }
    }

    public bool IsAchievement1
    {
        get { return m_isAchievement1; }
        set { m_isAchievement1 = value; }
    }

    public bool IsAchievement2
    {
        get { return m_isAchievement2; }
        set { m_isAchievement2 = value; }
    }

    public bool IsAchievement3
    {
        get { return m_isAchievement3; }
        set { m_isAchievement3 = value; }
    }

    public void UnLock()
    {
        m_isLock = false;
        m_time = m_limitTime;
    }

    [ContextMenu("ResetState")]
    public void ResetState()
    {
        m_onceClear = false;
        m_isLock = true;
        m_time = m_limitTime;
        m_isAchievement1 = false;
        m_isAchievement2 = false;
        m_isAchievement3 = false;
    }
}
