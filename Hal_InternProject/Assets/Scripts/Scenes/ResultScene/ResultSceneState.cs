using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResultSceneState : BaseSceneState
{
    public ResultScene m_scene { get; private set; }

    public override void Initialize(SceneController scene)
    {
        m_scene = (ResultScene)scene;
    }
}
