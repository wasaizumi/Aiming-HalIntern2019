using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSceneIdle : GameSceneState
{
    [SerializeField]
    private bool m_isClear = false;

    [SerializeField]
    private GameObject m_idleUI;
    [SerializeField]
    private TextMeshProUGUI m_timeText;
    [SerializeField]
    private TextMeshProUGUI m_timeLimitText;
    [SerializeField]
    private TextMeshProUGUI m_changeText;
    [SerializeField]
    private TextMeshProUGUI m_changeLimitText;

    private bool m_isLoaded = false;　//既に読み込まれた
    private float m_currentTime;

    public override void OnStart()
    {
        m_idleUI.SetActive(true);
        if (m_isLoaded) return;
        m_isLoaded = true;

        if (m_scene.m_systemData)
        {
            if (m_scene.m_systemData.CurrentStage.Difficulty == Difficulty.Easy)
                SoundObject.Instance.PlayBGM("EasyStage");
            if (m_scene.m_systemData.CurrentStage.Difficulty == Difficulty.Normal)
                SoundObject.Instance.PlayBGM("NormalStage");
            if (m_scene.m_systemData.CurrentStage.Difficulty == Difficulty.Hard)
                SoundObject.Instance.PlayBGM("HardStage");
        }

        m_idleUI.SetActive(true);
        m_currentTime = 0f;
    }

    public override void OnUpdate()
    {
        m_scene.m_actorController.OnUpdate();

        if (m_scene.m_systemData)
        {
            // Game Play LeftTop UI
            float limit = m_scene.m_systemData.CurrentStage.AchieveLimitTime;
            float time = m_scene.m_currentTime;
            float changeLimit = m_scene.m_systemData.CurrentStage.AchieveChangeNum;

            m_timeText.text = string.Format("{0:0}:{1:00}", Mathf.FloorToInt(time / 60f), Mathf.FloorToInt(time % 60));
            m_timeLimitText.text = string.Format("{0:0}:{1:00}", Mathf.FloorToInt(limit / 60f), Mathf.FloorToInt(limit % 60));

            m_changeText.text = string.Format("{0:0}", m_scene.m_changeNum > 999 ? 999 : m_scene.m_changeNum);
            m_changeLimitText.text = string.Format("{0:0}", changeLimit);
        }

        Result_Update();
        if (m_scene.m_commonInput.Input_Menu())
        {
            SoundObject.Instance.PauseBGM();
            SoundObject.Instance.PlaySE("Pause");
            m_scene.ChangeState<GameScenePause>();
        }
        m_scene.m_currentTime += Time.deltaTime;
    }

    public override void OnFixedUpdate()
    {
        m_scene.m_actorController.OnFixedUpdate();
    }

    public override void OnRelease()
    {
        m_idleUI.SetActive(false);
    }

    private void Result_Update()
    {
        if (m_scene.IsClear() || m_isClear)
        {
            m_scene.PlayerStop();
            m_scene.ChangeState<GameSceneClear>();
        }

        //ゲームのリザルトへ遷移する条件
        if (m_scene.IsDead())
        {
            m_scene.ChangeState<GameSceneFailed>();
        }
    }

    [ContextMenu("DeadState")]
    private void Dead()
    {
        m_scene.ChangeState<GameSceneFailed>();
    }
}
