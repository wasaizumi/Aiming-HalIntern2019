using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingStateResetGame : SettingState
{
    [SerializeField]
    private SceneController m_sceneController;
    [Header("UI")]
    [SerializeField]
    private GameObject m_checkColum;
    [SerializeField]
    private RectTransform m_cursor;
    [SerializeField]
    private RectTransform m_yes;
    [SerializeField]
    private RectTransform m_no;
    [SerializeField]
    private Color m_baseColor = Color.white;
    [SerializeField]
    private Color m_warningColor = Color.red;

    private bool m_IsClear = false;
    private MenuObject m_menuObject;
    private TextMeshProUGUI m_yesText;
    private TextMeshProUGUI m_noText;

    public override void Start()
    {
        m_menuObject = GetComponent<MenuObject>();
        m_yesText = m_yes.GetComponent<TextMeshProUGUI>();
        m_noText = m_no.GetComponent<TextMeshProUGUI>();
        base.Start();
    }

    public override void OnStart()
    {
        m_IsClear = false;
        SetColor();
        m_menuObject.SetSubmitColor();
        m_checkColum.SetActive(true);
    }

    public override void OnUpdate()
    {
        if (m_controller.InputHandler.Input_Cancel())
        {
            SoundObject.Instance.PlaySE("Cancel");
            m_controller.ChangeState<SettingStateIdle>();
        }

        ChangeSelect();

        if (m_controller.InputHandler.Input_Submit())
        {
            //削除
            if (m_IsClear)
            {
                m_controller.SystemData.Reset();
                m_sceneController.ChangeScene(SceneManager.GetActiveScene().name,2.0f,false,false);
                return;
            }
            SoundObject.Instance.PlaySE("Cancel");
            m_controller.ChangeState<SettingStateIdle>();
        }

        SetColor();
    }

    public override void OnRelease()
    {
        m_checkColum.SetActive(false);
        m_menuObject.SetDefaultColor();
    }

    private void SetColor()
    {
        if (m_IsClear)
        {
            m_cursor.position = m_yes.position;
            m_yesText.color = m_warningColor;
            m_noText.color = m_baseColor;
        }
        else
        {
            m_cursor.position = m_no.position;
            m_noText.color = m_warningColor;
            m_yesText.color = m_baseColor;
        }
    }

    private void ChangeSelect()
    {
        float vertical = m_controller.InputHandler.Input_LHorizontal();

        if (InputHandler.IsPositive(vertical) && m_IsClear)
        {
            SoundObject.Instance.PlaySE("CursorMove");
            m_IsClear = false;
        }
        if (InputHandler.IsNegative(vertical) && !m_IsClear)
        {
            SoundObject.Instance.PlaySE("CursorMove");
            m_IsClear = true;
        }
    }
}
