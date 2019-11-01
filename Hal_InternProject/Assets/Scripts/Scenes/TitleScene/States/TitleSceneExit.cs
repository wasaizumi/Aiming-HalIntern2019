using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneExit : TitleSceneState
{
    public override void OnStart()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
    UnityEngine.Application.Quit();
#endif
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnRelease()
    {
        
    }
}
