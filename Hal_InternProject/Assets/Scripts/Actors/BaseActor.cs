using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーやギミックを追加する時に継承する基底クラス

public class BaseActor : MonoBehaviour
{
    private ActorController m_actorController;
    public Rigidbody2D m_rigid2D { get; protected set; }
    private static float m_velocityMax = 15.0f;

    virtual public void OnStart()
    {
        m_rigid2D = GetComponent<Rigidbody2D>();
    }

    virtual public void OnUpdate()
    {

    }

    virtual public void OnFixedUpdate()
    {
        if (!m_rigid2D) return;

        if(m_rigid2D.velocity.magnitude > m_velocityMax)
        {
            m_rigid2D.velocity = m_rigid2D.velocity.normalized * m_velocityMax;
        }
    }

    public void Initialize(ActorController actorController)
    {
        m_actorController = actorController;
    }

    private void OnDestroy()
    {
        m_actorController.RemoveActor(this);
    }

    protected void AddObject(BaseActor new_actor)
    {
        m_actorController.AddObject(new_actor);
    }

}
