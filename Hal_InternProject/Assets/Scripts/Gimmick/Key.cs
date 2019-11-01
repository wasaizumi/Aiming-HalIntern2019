using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private Goal m_goal;

    [SerializeField] private GameObject m_effect_None;
    [SerializeField] private GameObject m_effect_N;
    [SerializeField] private GameObject m_effect_S;

    public bool m_isDead { get; private set; }

    private void OnDrawGizmos()
    {
        PoleCheck();
    }

    public void Start()
    {
        PoleCheck();
    }

    public void Update()
    {
        PoleCheck();
        if (m_isDead)
            Destroy(this.gameObject);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!collision.gameObject.TryGetComponent<Player>(out Player player)) return;

        SoundObject.Instance.PlaySE("Goal");
        m_goal.SetGoalOpen(true);

        Debug.Log("GOAL OPEN!!!");

        Destroy(this.gameObject);

    }

    public void Dead()
    {
        CameraShake shake = Camera.main.gameObject.GetComponent<CameraShake>();
        shake.Shake(0.1f, 0.5f);
        m_isDead = true;
    }

    private void PoleCheck()
    {
        var pole = GetComponent<PoleObject>();
        if(pole)
        {
            switch(pole.m_pole)
            {
                case PoleObject.Pole.None:
                    m_effect_None.SetActive(true);
                    m_effect_N.SetActive(false);
                    m_effect_S.SetActive(false);
                    break;
                case PoleObject.Pole.N:
                    m_effect_None.SetActive(false);
                    m_effect_N.SetActive(true);
                    m_effect_S.SetActive(false);
                    break;
                case PoleObject.Pole.S:
                    m_effect_None.SetActive(false);
                    m_effect_N.SetActive(false);
                    m_effect_S.SetActive(true);
                    break;
            }
        }
    }

}
