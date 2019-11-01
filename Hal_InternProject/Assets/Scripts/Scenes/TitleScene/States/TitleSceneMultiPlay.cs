using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneMultiPlay : TitleSceneState
{
    public override void OnStart()
    {
        m_scene.m_systemData.SetPlayMode(PlayMode.MultiPlay);
        m_scene.ChangeScene(m_scene.m_nextScene,2f,false,false);
    }

    public override void OnUpdate()
    {

    }

    public override void OnRelease()
    {

    }
}