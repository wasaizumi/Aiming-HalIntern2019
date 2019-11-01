using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetWaveSprite : MonoBehaviour
{
    public GameObject Pole_None;
    public GameObject Pole_N;
    public GameObject Pole_S;

    private GameObject m_effect;

    public GameObject SetPole(PoleObject.Pole pole)
    {
        if (m_effect != null)
            Destroy(m_effect.gameObject);

        if (pole == PoleObject.Pole.S)
            m_effect = Instantiate(Pole_S, transform.position, transform.rotation);
        else if (pole == PoleObject.Pole.N)
            m_effect = Instantiate(Pole_N, transform.position, transform.rotation);
        else
            m_effect = Instantiate(Pole_None, transform.position, transform.rotation);

        m_effect.transform.SetParent(gameObject.transform);

        return m_effect;

    }
}
