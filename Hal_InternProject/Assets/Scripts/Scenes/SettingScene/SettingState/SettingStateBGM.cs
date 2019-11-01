using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SettingSlider))]
public class SettingStateBGM : SettingState
{
    private MenuObject m_menuObject;
    private SettingSlider m_sliderScript;

    public override void Start()
    {
        m_menuObject = GetComponent<MenuObject>();
        m_sliderScript = GetComponent<SettingSlider>();
        m_sliderScript.SetValue(SoundObject.Instance.Param.BGMVolume * 0.01f);
        base.Start();
    }

    public override void OnStart()
    {
        m_sliderScript.SetValue(SoundObject.Instance.Param.BGMVolume * 0.01f);
        m_menuObject.SetSubmitColor();
    }

    public override void OnUpdate()
    {
        m_sliderScript.ChangeValue(m_controller.InputHandler);
        SoundObject.Instance.Param.BGMVolume = m_sliderScript.Value;

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
