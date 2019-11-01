using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 歩いているとき
public class PlayerState_Walk : PlayerState
{
    // 移動速度
    [SerializeField]
    private float m_moveSpeed;
    [SerializeField]
    private GameObject m_dashEffect;
    [SerializeField, Range(0.01f, 1.20f)]
    private float m_soundInterval = 1.0f;
    private float m_soundCount;

    public override void OnStart()
    {
        SoundObject.Instance.PlaySE("Walking");
        m_dashEffect.SetActive(true);
        m_soundCount = m_soundInterval;
    }

    public override void OnUpdate()
    {
        // 状態遷移があればこれ以上更新しない
        if (StateTransition())
            return;

        if (m_player.m_inputHandler.Input_Shot())
            m_player.Fire();

        // アニメーション
        Vector2 normal = m_player.m_castHit.normal.normalized;
        Vector2 crossDir = Vector3.Cross(Vector3.forward, normal).normalized;
        float moveValue = Vector2.Dot(crossDir, m_player.m_moveDir);

        Vector2 move = crossDir * m_player.m_moveAmount * moveValue;
        m_player.MoveAction(move * m_moveSpeed);

        if((move * m_moveSpeed).magnitude > 0.0f)
            WalkSE();

        Transform trans = m_player.transform;
        // 真反対だと計算が合わないので少しズラす
        if ((Vector2)trans.up == -normal)
            normal.x += 0.01f;

        // 現在立っている場所の向き調整
        trans.up = Vector2.Lerp(trans.up, normal, m_player.m_rotationSpeed * Time.deltaTime);
        trans.rotation = Quaternion.Euler(0.0f, 0.0f, trans.rotation.eulerAngles.z);

        // アニメーションをセット
        m_player.m_animator.SetFloat("moveAmount", moveValue);
    }

    public override void OnRelease()
    {
        m_dashEffect.SetActive(false);
    }


    protected override bool StateTransition()
    {
        if (m_player.m_onGround)
        {
            if (m_player.m_moveAmount <= 0.0f)
            {
                m_player.ChangeState<PlayerState_Idle>();
                return true;
            }
        }
        else
        {
            m_player.ChangeState<PlayerState_Flying>();
            return true;
        }

        return false;
    }

    private void WalkSE()
    {
        m_soundCount -= Time.deltaTime;
        if (m_soundCount < 0)
        {
            m_soundCount = m_soundInterval;
            SoundObject.Instance.PlaySE("Walking");
        }

    }

}
