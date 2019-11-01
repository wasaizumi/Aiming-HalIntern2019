using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetWaveTail : MonoBehaviour
{
    [SerializeField]
    private PoleEffectData m_poleEffectData;

    public GameObject CreateEffect(PoleObject.Pole pole, Transform parent)
    {
        return Instantiate(m_poleEffectData.GetPoleEffects(pole), parent.position, Quaternion.identity);
    }
}
