using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingState : MonoBehaviour
{
    public SettingController m_controller { get; private set; }

    public void Initialize(SettingController controller)
    {
        m_controller = controller;
    }

    public virtual void Start()
    {

    }

    public virtual void OnStart()
    {
        
    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnRelease()
    {

    }
}
