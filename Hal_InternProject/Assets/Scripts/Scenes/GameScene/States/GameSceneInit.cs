using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneInit : GameSceneState
{
    public override void OnStart()
    {
        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        while (FadeController.IsActive)
            yield return null;
        m_scene.ChangeState<GameSceneEventIdle>();
        yield break;
    }
}
