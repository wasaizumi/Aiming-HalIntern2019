using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TitleSceneState : BaseSceneState
{
    public TitleScene m_scene { get; private set; }

    public override void Initialize(SceneController scene)
    {
        m_scene = (TitleScene)scene;
    }
}
