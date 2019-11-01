using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    ParticleSystem m_particleSystem;
    void Awake()
    {
        m_particleSystem = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (m_particleSystem != null && !m_particleSystem.IsAlive())
        {
            Destroy(this.gameObject);
        }
    }
}
