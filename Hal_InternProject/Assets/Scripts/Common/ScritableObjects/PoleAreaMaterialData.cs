using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/PoleMaterialData", fileName = "PoleMaterial")]
public class PoleAreaMaterialData : ScriptableObject
{
    [SerializeField]
    private PoleMaterials m_materials = new PoleMaterials();

    public PoleMaterials m_Materials
    {
        get { return m_materials; }
    }
}
