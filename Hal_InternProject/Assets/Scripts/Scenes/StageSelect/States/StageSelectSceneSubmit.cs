using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectSceneSubmit : StageSelectSceneState
{
    [SerializeField]
    private string m_nextScene;

    public override void OnStart()
    {
        m_scene.ChangeScene(m_nextScene,2f,false,true);
        m_scene.ChangeState<StageSelectExit>();
    }
}
