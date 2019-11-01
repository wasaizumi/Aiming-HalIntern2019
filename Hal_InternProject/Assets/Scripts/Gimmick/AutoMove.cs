using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] //SendMessageでエラーが出ないように
public class AutoMove : BaseActor
{
    [SerializeField]
    private float m_moveSpeed;
    [SerializeField]
    private float m_stopTime;

    [SerializeField]
    private Transform m_point1;
    [SerializeField]
    private Transform m_point2;

    private Transform m_currentTargetPoint;
    private Transform m_moveTransform;
    private bool m_isMove = true;
    private float m_stopValue;

    private void OnDrawGizmos()
    {
        LineRenderer lineRen = GetComponent<LineRenderer>();
        if (!lineRen || !m_point1 || !m_point2) return;

        Vector3 pos1 = m_point1.position; pos1.z += 0.1f;
        Vector3 pos2 = m_point2.position; pos2.z += 0.1f;
        lineRen.SetPosition(0, pos1);
        lineRen.SetPosition(1, pos2);

        m_point1.right = pos2 - pos1;
        m_point2.right = -(pos1 - pos2);
    }

    // ターゲットポイント反転
    private void SwitchTarget()
    {
        m_currentTargetPoint = (m_currentTargetPoint == m_point1) ? m_point2 : m_point1;
    }

    public string TargetPointName()
    {
        return (m_currentTargetPoint == m_point1) ? "Point1" : "Point2";
    }

    public override void OnStart()
    {
        // 自分のポイント以外の子を一つだけ探して動かすオブジェクトとする
        int count = transform.childCount;
        for(int i = 0;i < count;i++)
        {
            Transform child = transform.GetChild(i);
            if (child == m_point1) continue;
            if (child == m_point2) continue;
            m_moveTransform = child;
            break;
        }

        m_currentTargetPoint = m_point1;
    }

    public override void OnFixedUpdate()
    {
        if (m_isMove)
            Move();
        else
            Stop();
    }

    private void Move()
    {
        Vector2 pos = m_moveTransform.localPosition;
        Vector2 targetPos = m_currentTargetPoint.localPosition;
        Vector2 moveDir = targetPos - pos;
        float length = moveDir.magnitude;
        moveDir.Normalize();

        float moveLength = m_moveSpeed * Time.deltaTime;
        if(length <= moveLength)
        {
            m_isMove = false;
            moveLength = length;
        }

        m_moveTransform.localPosition = pos + moveDir * moveLength;
    }

    private void Stop()
    {
        m_stopValue += Time.deltaTime;
        if(m_stopValue >= m_stopTime)
        {
            m_isMove = true;
            m_stopValue = 0.0f;
            SwitchTarget();
        }
    }
}


