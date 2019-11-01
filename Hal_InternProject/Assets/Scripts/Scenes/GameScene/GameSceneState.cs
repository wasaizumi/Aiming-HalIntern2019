using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameSceneState : BaseSceneState
{
    public GameScene m_scene { get; private set; }

    public override void Initialize(SceneController scene)
    {
        m_scene = scene as GameScene;
    }
}
