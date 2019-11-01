using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneSetting : TitleSceneState
{
    [SerializeField]
    private SettingController m_settingController;

    public override void Start()
    {
        m_settingController.Start();
        m_settingController.m_exitAction += delegate ()
        {
            m_scene.ChangeState<TitleSceneIdle>();
        };
    }

    public override void OnStart()
    {
        m_settingController.SetInputHandler(m_scene.m_commonInput);
        m_settingController.SetSystemData(m_scene.m_systemData);
        m_settingController.OnStart();
    }

    public override void OnUpdate()
    {
        m_settingController.OnUpdate();
    }

    public override void OnRelease()
    {
        m_settingController.OnRelase();
    }
}
