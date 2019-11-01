using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_states;
    [SerializeField]
    private BaseSceneState m_currentState;
    public SystemData m_systemData;

    private List<BaseSceneState> m_sceneStateList;
    private FadeController m_fadeController;

    protected virtual void Start()
    {
        LoadState();

        GameObject instance = Instantiate(Resources.Load("Prefab/Scene/FadeCanvas")) as GameObject;
        m_fadeController = instance.GetComponent<FadeController>();

        m_currentState = m_sceneStateList[0];
        foreach(var state in m_sceneStateList)
        {
            state.Initialize(this);
        }

        m_currentState.OnStart();
    }

    protected virtual void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
            ChangeScene("TitleScene", 0.0f,true,false);
        m_currentState.OnUpdate();
    }

    protected virtual void FixedUpdate()
    {
        m_currentState.OnFixedUpdate();
    }

    public void ChangeScene(string nextSceneName,float fadeTime,bool FullFadeIn,bool FullFadeOut)
    {
        SoundObject.Instance.StopBGM(1.0f);
        m_fadeController.ChangeScene(nextSceneName,fadeTime,FullFadeIn,FullFadeOut);
    }
    public void ChangeState<T>()
    {
        foreach(var state in m_sceneStateList)
        {
            //Typeが違う
            if (state.GetType() != typeof(T)) continue;
            if(m_currentState)
                m_currentState.OnRelease();
                
            m_currentState = state;
            m_currentState.OnStart();
        }
    }

    [ContextMenu("LoadState")]
    public void LoadState()
    {
        var components = m_states.GetComponentsInChildren<BaseSceneState>();
        m_sceneStateList = new List<BaseSceneState>();
        foreach (var component in components)
        {
            m_sceneStateList.Add(component);
        }
    }
}
