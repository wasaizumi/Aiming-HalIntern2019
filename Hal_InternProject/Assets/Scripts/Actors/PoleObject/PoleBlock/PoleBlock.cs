using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleBlock : PoleObject
{
    public enum Type
    {
        Move_Change,
        NoMove_Change,
        NoMove_NoChange,
        TypeMax
    }

    public Type m_type;

    private new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (!m_rigid2D) m_rigid2D = GetComponent<Rigidbody2D>();
        TypeChange();
    }

    public void OnTypeChange(Type type)
    {
        m_type = type;
        TypeChange();
    }

    public override bool IsPoleChange()
    {
        if (m_type == Type.NoMove_NoChange)
            return false;

        return true;
    }

    private void TypeChange()
    {
        switch(m_type)
        {
            case Type.Move_Change:
                {
                    m_rigid2D.constraints = RigidbodyConstraints2D.None;
                }
                break;
            case Type.NoMove_Change:
                {
                    m_rigid2D.constraints = RigidbodyConstraints2D.FreezeAll;
                }
                break;
            case Type.NoMove_NoChange:
                {
                    m_rigid2D.constraints = RigidbodyConstraints2D.FreezeAll;
                }
                break;
        }
    }

    // 磁力の範囲に入った時に呼ばれる
    public override void OnCircleTrigger2D(PoleArea mine, PoleArea other)
    {
        if (m_pole == Pole.None) return;
        if (other.m_poleObject.m_pole == Pole.None) return;
        if (m_type == Type.NoMove_Change || m_type == Type.NoMove_NoChange) return;

        Vector2 dir = other.transform.position - mine.transform.position;
        float distance = dir.magnitude;
        float border = mine.m_radius + other.m_radius;
        if (border <= 0.0f) return;

        Vector2 forceDir;
        forceDir = (m_pole == other.m_poleObject.m_pole) ? -dir : dir;

        Vector2 add = forceDir.normalized * (1.0f - distance / border);
        AddPoleForce(add);
    }
}
