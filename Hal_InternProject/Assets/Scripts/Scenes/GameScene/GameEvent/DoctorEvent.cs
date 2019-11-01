using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorEvent : GameEvent
{
    [SerializeField]
    private GameObject[] m_displayObjs;

    private int m_currentNum = 0;
    private GameObject m_currentObj;

    public override void OnStart()
    {
        this.gameObject.SetActive(true);
        m_currentObj = m_displayObjs[m_currentNum];
        m_currentObj.SetActive(true);
    }

    public override void OnUpdate()
    {
        if (m_scene.m_commonInput.Input_Submit())
            this.ChangeDisplay();
    }

    public override void OnRelease()
    {
        m_currentObj.SetActive(false);
        this.gameObject.SetActive(false);
    }

    private void ChangeDisplay()
    {
        m_currentObj.SetActive(false);
        m_currentNum++;
        if (m_currentNum >= m_displayObjs.Length)
        {
            this.EventEnd();
            return;
        }
        m_currentObj = m_displayObjs[m_currentNum];
        m_currentObj.SetActive(true);
    }
}
