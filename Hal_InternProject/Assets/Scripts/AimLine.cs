using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLine : MonoBehaviour
{
    private MeshRenderer m_meshRenderer;
    [SerializeField]
    private Player m_player;

    private PoleObject.Pole m_pole;

    private void Awake()
    {
        if (m_meshRenderer == null)
            m_meshRenderer = this.gameObject.GetComponent<MeshRenderer>();

        if (m_player)
            PoleColorChange(m_player.m_pole);
    }

    private void Update()
    {
        if (!m_player)
            return;

        if(m_pole != m_player.m_pole)
        {
            PoleColorChange(m_player.m_pole);
        }
        
    }

    private void PoleColorChange(PoleObject.Pole pole)
    {

        if (pole == PoleObject.Pole.N)
            m_meshRenderer.material.color = new Color(0.75f, 0.1f, 0.1f, 0.0f);
        else if (pole == PoleObject.Pole.S)
            m_meshRenderer.material.color = new Color(0.1f, 0.6f, 0.75f, 0.0f);
        else
            m_meshRenderer.material.color = new Color(0.6f, 0.4f, 0.7f, 0.0f);

        m_pole = pole;
    }
}
