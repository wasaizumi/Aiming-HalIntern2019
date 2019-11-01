using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneFailed : GameSceneState
{
    [SerializeField,Tooltip("失敗時のUI")]
    private GameObject m_failedUI;
    [SerializeField]
    private string m_nextSceneName;

    public override void OnStart()
    {
        SoundObject.Instance.StopBGM(1.0f);
        SoundObject.Instance.PlaySE("GameOver");
        m_failedUI.SetActive(true);
    }

    public override void OnUpdate()
    {
        //リトライ
        if (m_scene.m_commonInput.Input_Submit())
        {
            SoundObject.Instance.PlayBGM();
            SoundObject.Instance.PlaySE("StageIn");
            m_scene.ChangeScene(SceneManager.GetActiveScene().name, 2f,false,true);
            m_scene.ChangeState<GameSceneExit>();
        }
        //ステージセレクトへ
        if (m_scene.m_commonInput.Input_Cancel()) 
        {
            SoundObject.Instance.PlaySE("Decide");
            m_scene.ChangeScene(m_nextSceneName, 4f,false,false);
            m_scene.ChangeState<GameSceneExit>();
        }
            
    }
}
