using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StageSelectSceneState : BaseSceneState
{
    public StageSelectScene m_scene { get; private set; }

    public override void Initialize(SceneController scene)
    {
        m_scene = (StageSelectScene)scene;
    }
}
