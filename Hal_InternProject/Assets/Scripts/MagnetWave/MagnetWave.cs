using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetWave : BaseActor
{
    private PoleObject m_player;

    private Vector2 m_front;
    public float m_speed;

    public float m_maxLife;
    private float m_life;

    [SerializeField]
    private MagnetWaveTail m_waveTail;
    private GameObject m_waveTailEffect;
    [SerializeField]
    private MagnetSwap m_magnetSwap;

    public void Shot()
    {
        m_life = m_maxLife;

        m_magnetSwap.gameObject.SetActive(false);

        if (!m_waveTailEffect)
        {
            m_waveTailEffect = m_waveTail.CreateEffect(m_player.m_pole, transform);
            var energy = m_waveTailEffect.GetComponent<MagnetEnergy>();
            // エフェクトの向きを直接変える
            float angle = Mathf.Atan2(-m_front.y, m_front.x);
            foreach (var per in energy.m_particleList)
            {
                per.startRotation = angle;
            }
        }
    }

    public override void OnUpdate()
    {
        bool hit = m_magnetSwap.gameObject.activeSelf;

        if (hit)
        {
            m_magnetSwap.OnUpdate();
            if (!m_magnetSwap.gameObject.activeSelf)
                SetActiveFalse();
        }

        if (!this.gameObject.activeSelf)
        {
            if (m_waveTailEffect)
                m_waveTailEffect.SetActive(false);
            return;
        }

        Vector3 pos = this.transform.position;
        pos.x += m_front.x * m_speed;
        pos.y += m_front.y * m_speed;
        this.transform.position = pos;

        if (m_waveTailEffect)
        {
            m_waveTailEffect.SetActive(true);
            m_waveTailEffect.transform.position = pos;
        }

        m_life -= Time.deltaTime;
        if (m_life <= 0.0f)
            SetActiveFalse();
    }

    public void SetFrontVector(Vector2 front)
    {
        m_front = front;
    }

    //プレイヤーをセットする同時にpoleもセットする
    public void SetPlayer(PoleObject actor)
    {
        m_player = actor;
    }

    private void SetActiveFalse()
    {
        this.gameObject.SetActive(false);
        Destroy(m_waveTailEffect, 2.0f);
        m_waveTailEffect = null;
    }

    public bool IsActive()
    {
        if (gameObject.activeSelf)
            return true;
        if (m_magnetSwap.gameObject.activeSelf)
            return true;

        return false;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_magnetSwap.gameObject.activeSelf) return;
        if (m_player.gameObject == collision.gameObject) return;

        bool non_active_object =
            collision.gameObject.TryGetComponent<Wall_Wire>(out Wall_Wire Wall) ||
            collision.gameObject.TryGetComponent<Spine>(out Spine Spine);

        if (non_active_object) return;

        if (!collision.gameObject.TryGetComponent<PoleObject>(out PoleObject others))
            others = collision.gameObject.GetComponentInParent<PoleObject>();

        if (others)
        {
            // 変更できるブロックの場合
            if (others.IsPoleChange())
            {
                // 同極の場合は失敗
                if (others.m_pole == m_player.m_pole)
                {
                    SoundObject.Instance.PlaySE("Magnet_Miss");
                    others.PoleChange(others.m_pole);
                }
                // 成功パターン
                else
                {
                    SoundObject.Instance.PlaySE("MagnetChange");

                    m_magnetSwap.gameObject.SetActive(true);
                    m_magnetSwap.SetSwapObject(m_player, others);
                    m_magnetSwap.OnStart();
                }
            }
            // 失敗した時
            else
            {
                SoundObject.Instance.PlaySE("Reflect");
                others.PoleChange(others.m_pole);
            }
        }

        SetActiveFalse();
    }

}
