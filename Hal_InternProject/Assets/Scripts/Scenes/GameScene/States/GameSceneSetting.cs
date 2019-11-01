using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneSetting : GameSceneState
{
    [SerializeField]
    private SettingController m_controller;

    public override void Start()
    {
        m_controller.Start();
        m_controller.m_exitAction += delegate ()
        {
            m_scene.ChangeState<GameScenePause>();
        };
    }

    public override void OnStart()
    {
        m_controller.SetInputHandler(m_scene.m_commonInput);
        m_controller.OnStart();
    }

    public override void OnUpdate()
    {
        m_controller.OnUpdate();
    }

    public override void OnRelease()
    {
        m_controller.OnRelase();
    }
}
