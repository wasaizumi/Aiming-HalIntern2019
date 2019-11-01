using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 何も操作しないとき
public class PlayerState_Idle : PlayerState
{
    public override void OnStart()
    {
        m_player.m_animator.SetFloat("moveAmount", 0.0f);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        // 状態遷移があればこれ以上更新しない
        if (StateTransition())
            return;

        // 弾発射
        if (m_player.m_inputHandler.Input_Shot())
            m_player.Fire();

        Vector2 normal = m_player.m_castHit.normal.normalized;
        Transform trans = m_player.transform;

        // 真反対だと計算が合わないので少しズラす
        if ((Vector2)trans.up == -normal)
            normal.x += 0.01f;

        trans.up = Vector2.Lerp(trans.up, normal, m_player.m_rotationSpeed * Time.deltaTime);
        trans.rotation = Quaternion.Euler(0.0f, 0.0f, trans.rotation.eulerAngles.z);
    }

    protected override bool StateTransition()
    {
        if (m_player.m_onGround)
        {
            if (m_player.m_moveAmount > 0.0f)
            {
                m_player.ChangeState<PlayerState_Walk>();
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

}
