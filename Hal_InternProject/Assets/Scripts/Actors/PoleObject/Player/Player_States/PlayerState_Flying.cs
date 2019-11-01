using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Flying : PlayerState
{
    // 移動速度
    [SerializeField]
    private float m_moveSpeed;
    [SerializeField]
    private GameObject m_landingEffect;

    public override void OnStart()
    {
        m_player.m_animator.SetFloat("moveAmount", 0.0f);
    }

    public override void OnUpdate()
    {
        // 状態遷移があればこれ以上更新しない
        if (SceneTransition())
            return;

        if (m_player.m_inputHandler.Input_Shot())
            m_player.Fire();

        m_player.MoveAction(Vector2.right * m_player.m_horisontal * m_moveSpeed);
    }

    public override void OnRelease()
    {
        GameObject per = Instantiate<GameObject>(m_landingEffect, m_player.m_castHit.point, m_player.transform.rotation);
        Destroy(per, 2);
    }

    private bool SceneTransition()
    {
        if (!m_player.m_onGround) return false;

        if (m_player.m_moveAmount <= 0.0f)
        {
            SoundObject.Instance.PlaySE("Collide");
            m_player.ChangeState<PlayerState_Idle>();
            return true;
        }
        else
        {
            SoundObject.Instance.PlaySE("Collide");
            m_player.ChangeState<PlayerState_Walk>();
            return true;
        }
    }

}
