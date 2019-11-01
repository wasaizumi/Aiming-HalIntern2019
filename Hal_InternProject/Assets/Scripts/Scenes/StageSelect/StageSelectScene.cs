using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectScene : SceneController
{
    [Header("StageSelectScene")]
    public InputHandler m_comonInput;

    [SerializeField,Space(8)]
    private StageSelectSceneIdle m_stageSelectSceneIdle;
}
