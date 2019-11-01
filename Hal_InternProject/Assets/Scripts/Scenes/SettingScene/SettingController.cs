using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingController : MonoBehaviour
{
    public event System.Action m_exitAction = delegate{};

    [SerializeField]
    private SettingState m_currentState;
    [SerializeField,NonEditable]
    private List<SettingState> m_settingStates;
    [SerializeField]
    private RectTransform[] m_selectMenu;

    [Header("UI")]
    [SerializeField]
    private RectTransform m_cursor;

    public InputHandler InputHandler { get; private set; }
    public SystemData SystemData { get; private set; }

    public int MenuNum { get { return m_selectMenu.Length; } }

    public void SetInputHandler(InputHandler inputHandler)
    {
        InputHandler = inputHandler;
    }

    public void SetSystemData(SystemData systemData)
    {
        SystemData = systemData;
    }
    
    public void Start()
    {
        foreach (var obj in m_selectMenu)
            m_settingStates.Add(obj.GetComponent<SettingState>());

        foreach (var state in m_settingStates)
            state.Initialize(this);

        m_currentState = m_settingStates[0];
        SetCursor(0);
    }

    public void OnStart()
    {
        foreach (var state in m_settingStates)
        {
            if (state.GetType() != typeof(SettingStateIdle)) continue;
            var idle = state as SettingStateIdle;
            idle.ResetSelect();
        }
        m_currentState.OnStart();
        this.gameObject.SetActive(true);
    }

    public void OnUpdate() 
    {
        m_currentState.OnUpdate();
    }

    public void OnRelase()
    {
        m_currentState.OnRelease();
    }

    public void ChangeState<T>()
    {
        foreach(var state in m_settingStates)
        {
            if (state.GetType() != typeof(T))continue;
            if (m_currentState) m_currentState.OnRelease();
            m_currentState = state;
            m_currentState.OnStart();
        }
    }

    public void ChangeMenuState(int num)
    {
        if (num >= MenuNum) return;
        m_currentState.OnRelease();
        m_currentState = m_selectMenu[num].GetComponent<SettingState>();
        m_currentState.OnStart();
    }

    public void Exit()
    {
        m_currentState.OnRelease();
        m_exitAction();
        this.gameObject.SetActive(false);
    }

    public void SetCursor(int nStateNum)
    {
        if (nStateNum >= MenuNum) return;
        Vector3 pos = m_cursor.position;
        pos.y = m_selectMenu[nStateNum].transform.position.y;
        m_cursor.position = pos;
    }
}
