using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PoleMaterials
{
    public Material Pole_None;
    public Material Pole_N;
    public Material Pole_S;
}

public class PoleAreaMaterial : MonoBehaviour
{
    private MeshRenderer m_meshRenderer;

    [SerializeField]
    private PoleAreaMaterialData m_poleMaterial;

    private void Start()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
    }

    public void PoleChange(PoleObject poleObject)
    {
        if (!m_meshRenderer)
            m_meshRenderer = GetComponent<MeshRenderer>();

        MaterialChange(m_poleMaterial.m_Materials, poleObject.m_pole);
    }

    protected void MaterialChange(PoleMaterials materials, PoleObject.Pole pole)
    {
        if (pole == PoleObject.Pole.S)
            m_meshRenderer.material = materials.Pole_S;
        else if (pole == PoleObject.Pole.N)
            m_meshRenderer.material = materials.Pole_N;
        else
            m_meshRenderer.material = materials.Pole_None;
    }
}
