using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingStateIdle : SettingState
{
    private int m_nSelect =0;
    private float m_waitTime = 0f;

    public void ResetSelect()
    {
        m_nSelect = 0;
    }

    public override void OnStart()
    {
        m_controller.SetCursor(m_nSelect);
    }

    public override void OnUpdate()
    {
        if (m_controller.InputHandler.Input_Submit())
        {
            SoundObject.Instance.PlaySE("Decide");
            m_controller.ChangeMenuState(m_nSelect);
        }
        if (m_controller.InputHandler.Input_Cancel())
        {
            SoundObject.Instance.PlaySE("Cancel");
            m_controller.Exit();
        }

        if(m_waitTime >= m_controller.InputHandler.WaitTime)
            ChangeSelect();
        m_waitTime += 0.02f;
    }

    public override void OnRelease()
    {
        
    }

    private void ChangeSelect()
    {
        int oldSelect = m_nSelect;

        float vertical = Input.GetAxisRaw(m_controller.InputHandler.LVertical);

        if (InputHandler.IsPositive(vertical)) m_nSelect--;
        if (InputHandler.IsNegative(vertical)) m_nSelect++;

        if (oldSelect == m_nSelect) return;
        if (m_nSelect < 0) m_nSelect = m_controller.MenuNum - 1;
        if (m_nSelect >= m_controller.MenuNum) m_nSelect = 0;

        SoundObject.Instance.PlaySE("CursorMove");
        m_controller.SetCursor(m_nSelect);
        m_waitTime = 0f;
    }
}
