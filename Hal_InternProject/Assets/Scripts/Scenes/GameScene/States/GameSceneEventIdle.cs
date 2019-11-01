using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneEventIdle : GameSceneState
{
    [SerializeField]
    private int m_currentEventNum = 0;
    [SerializeField]
    private GameEvent m_currentEvent;

    public override void OnStart()
    {
        SoundObject.Instance.PlayBGM("Doctor");
        if (m_scene.m_gameEvents.Count == 0) 
        {
            m_scene.ChangeState<GameSceneIdle>();
            return;
        }
        Time.timeScale = 0;
        m_currentEvent = m_scene.m_gameEvents[m_currentEventNum];
        m_currentEvent.OnStart();
    }

    public override void OnUpdate()
    {
        if (m_currentEvent.IsEnd) ChangeEvent();
        m_currentEvent.OnUpdate();
    }

    private void ChangeEvent()
    {
        m_currentEvent.OnRelease();
        m_currentEventNum++;

        if (m_currentEventNum >= m_scene.m_gameEvents.Count)
        {
            Time.timeScale = 1;
            SoundObject.Instance.StopBGM(0.5f);
            m_scene.ChangeState<GameSceneIdle>();
            return;
        }
        m_currentEvent = m_scene.m_gameEvents[m_currentEventNum];
        m_currentEvent.OnStart();
    }
}
