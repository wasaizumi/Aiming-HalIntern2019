using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : BaseActor
{
    [SerializeField]
    private bool m_goalCheck = false;
    [SerializeField]
    private bool m_goalOpen;

    [SerializeField]
    private GimmickAnimation m_iamgeData;

    [SerializeField]
    private Sprite m_closed;

    private SpriteRenderer m_doorSprite;

    private float m_doorCounter;

    public override void OnStart()
    {
        m_doorSprite = GetComponent<SpriteRenderer>();
        m_doorSprite.sprite = m_closed;
    }

    public override void OnUpdate()
    {
        if (m_goalOpen)
        {
            m_doorCounter = Mathf.Clamp(m_doorCounter + Time.deltaTime, 0.0f, m_iamgeData.Speed * (m_iamgeData.AnimNum - 1));
            m_doorSprite.sprite = m_iamgeData.GetAnimImage(Mathf.RoundToInt(m_doorCounter / m_iamgeData.Speed));

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!m_goalOpen)
            return;

        if (!collision.TryGetComponent<Player>(out Player player) && !m_goalCheck) return;

        //ゴールフラグ処理追加予定地
        m_goalCheck = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Player>(out Player player)) return;

        //ゴールフラグ処理追加予定地
        m_goalCheck = false;

    }

    public bool GetGoalCheck()
    {
        return m_goalCheck;
    }

    public void SetGoalOpen(bool open_or_close)
    {
        m_goalOpen = open_or_close;
        if(!m_goalOpen)
            m_doorSprite.sprite = m_closed;
        else
        {
            m_doorSprite.sprite = m_iamgeData.GetAnimImage(0);
            m_doorCounter = 0.0f;
        }
    }
}
