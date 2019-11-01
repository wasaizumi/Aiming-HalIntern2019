using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    bool m_isCursorMove = false;
    Vector3 m_posOld = Vector3.zero;

    public void OnUpdate()
    {
        m_isCursorMove = (m_posOld == gameObject.transform.position) ? false : true;

        m_posOld = gameObject.transform.position;

        // マウス座標取得
        Vector3 m_position = Input.mousePosition;
        // マウス座標をスクリーン座標に変換
        Vector3 m_screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(m_position);
        m_screenToWorldPointPosition.z = 0.0f;

        // カーソル座標を移動する
        gameObject.transform.position = m_screenToWorldPointPosition;

    }

    public bool GetIsCursorCheck()
    {
        return m_isCursorMove;
    }
}
