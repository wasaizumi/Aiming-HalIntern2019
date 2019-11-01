using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleAreaEffect : MonoBehaviour
{
    [SerializeField] private GameObject m_pole_None;
    [SerializeField] private GameObject m_pole_S;
    [SerializeField] private GameObject m_pole_N;
    [SerializeField] private ParticleSystem m_changeEffect; 

    public void PoleChange(PoleObject poleObject)
    {
        SetActiveEffect(poleObject.m_pole);
        m_changeEffect.Play();
    }

    public void Gizomos(PoleObject poleObject)
    {
        SetActiveEffect(poleObject.m_pole);
    }

    public GameObject GetActiveEffect()
    {
        if (m_pole_None.activeInHierarchy)
            return m_pole_None;
        if (m_pole_N.activeInHierarchy)
            return m_pole_N;
        if (m_pole_S.activeInHierarchy)
            return m_pole_S;

        return null;
    }

    private void SetActiveEffect(PoleObject.Pole pole)
    {
        switch(pole)
        {
            case PoleObject.Pole.None:
                m_pole_None.SetActive(true);
                m_pole_N.SetActive(false);
                m_pole_S.SetActive(false);
                break;
            case PoleObject.Pole.N:
                m_pole_None.SetActive(false);
                m_pole_N.SetActive(true);
                m_pole_S.SetActive(false);
                break;
            case PoleObject.Pole.S:
                m_pole_None.SetActive(false);
                m_pole_N.SetActive(false);
                m_pole_S.SetActive(true);
                break;
        }
    }
}
