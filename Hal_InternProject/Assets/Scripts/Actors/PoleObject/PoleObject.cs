using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleObject : BaseActor
{
    public enum Pole
    {
        None,
        S,
        N
    }

    public Pole m_pole;

    protected Vector2 m_mixForce;
    public PoleSprite[] m_poleSprites { get; private set; }
    private PoleArea m_poleArea;

    public static float poleCoefficient = 1.0f;
    public float m_forceValue = 100.0f;

    // インスペクターが変更せれると呼ばれる
    protected void OnDrawGizmos()
    {
        PoleSprite[] sprites = GetComponentsInChildren<PoleSprite>();
        foreach (var s in sprites)
        {
            s.PoleChange(this);
        }

        PoleArea area = GetComponentInChildren<PoleArea>();
        if (area)
            area.Gizomos(this);
    }

    public override void OnStart()
    {
        base.OnStart();

        m_poleSprites = GetComponentsInChildren<PoleSprite>();
        foreach (var s in m_poleSprites)
        {
            s.PoleChange(this);
        }

        m_poleArea = GetComponentInChildren<PoleArea>();
        if (m_poleArea) m_poleArea.SetPoleObject(this);
    }

    public override void OnFixedUpdate()
    {
        // OnTriggerStay() で集めた合力をAddForce()
        m_rigid2D.AddForce(m_mixForce * m_forceValue, ForceMode2D.Force);
        m_mixForce = Vector2.zero;

        base.OnFixedUpdate();
    }

    public virtual void PoleChange(Pole pole)
    {
        // 自分を変える
        m_pole = pole;

        // 子要素に変更を教える
        foreach (var s in m_poleSprites)
        {
            s.PoleChange(this);
        }

        m_poleArea.PoleChange();
    }

    public virtual bool IsPoleChange()
    {
        return true;
    }

    public GameObject GetCreateCircleEffect()
    {
        return m_poleArea.GetCreateCircleEffect();
    }

    public void AddPoleForce(Vector2 add)
    {
        m_mixForce += add;
    }

    public virtual void OnCircleTrigger2D(PoleArea mine, PoleArea other)
    {
        
    }
}
