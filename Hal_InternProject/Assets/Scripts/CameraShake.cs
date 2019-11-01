using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 initPos;
    float m_amount = 0;
    Vector2 m_veclocity;

    private void Awake()
    {
        initPos = transform.position;
    }

    //Shakeする激しさと時間の長さを渡してください
    public void Shake(float amt, float len)
    {
        m_amount = amt;

        InvokeRepeating("DoShake", 0.0f, 0.05f);
        Invoke("StopShake", len);
    }

    private void Update()
    {
        transform.position = initPos + (Vector3)m_veclocity;

        m_veclocity *= 0.9f;
    }

    private void DoShake()
    {
        Vector3 pos = transform.position;

        Vector2 move = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        m_veclocity += move * m_amount;
    }

    private void StopShake()
    {
        CancelInvoke("DoShake");
        transform.position = new Vector3(0.0f, 0.0f, -10.0f);
    }

}
