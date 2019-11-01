using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneSinglePlay : TitleSceneState
{
    public override void OnStart()
    {
        m_scene.m_systemData.SetPlayMode(PlayMode.SinglePlay);
        m_scene.ChangeScene(m_scene.m_nextScene,1f,false,false);
    }

    public override void OnUpdate()
    {

    }

    public override void OnRelease()
    {

    }
}