using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PoleEffectType
{
    public GameObject Pole_None;
    public GameObject Pole_N;
    public GameObject Pole_S;
}

[CreateAssetMenu(menuName = "GameData/PoleEffectData", fileName = "PoleEffect")]
public class PoleEffectData : ScriptableObject
{
    [SerializeField]
    private PoleEffectType m_effects = new PoleEffectType();

    public GameObject GetPoleEffects(PoleObject.Pole pole)
    {
        if (pole == PoleObject.Pole.N)
            return m_effects.Pole_N;
        if (pole == PoleObject.Pole.S)
            return m_effects.Pole_S;

        return m_effects.Pole_None;
    }
}
