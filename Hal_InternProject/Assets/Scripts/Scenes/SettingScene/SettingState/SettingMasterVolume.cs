using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SettingSlider))]
public class SettingMasterVolume : SettingState
{
    private MenuObject m_menuObject;
    private SettingSlider m_sliderScript;

    public override void Start()
    {
        m_menuObject = GetComponent<MenuObject>();
        m_sliderScript = GetComponent<SettingSlider>();
        m_sliderScript.SetValue(AudioListener.volume);
        base.Start();
    }

    public override void OnStart()
    {
        m_sliderScript.SetValue(AudioListener.volume);
        m_menuObject.SetSubmitColor();
    }

    public override void OnUpdate()
    {
        m_sliderScript.ChangeValue(m_controller.InputHandler);
        AudioListener.volume = m_sliderScript.NormalizedValue;

        if (m_controller.InputHandler.Input_Submit())
        {
            SoundObject.Instance.PlaySE("Decide");
            m_controller.ChangeState<SettingStateIdle>();
        }
        if (m_controller.InputHandler.Input_Cancel())
        {
            SoundObject.Instance.PlaySE("Cancel");
            m_controller.ChangeState<SettingStateIdle>();
        }
    }

    public override void OnRelease()
    {
        m_menuObject.SetDefaultColor();
    }
}
