using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundGeneral", menuName = "Sound/General")]
public class SoundGeneral : ScriptableObject
{
    [Header("BGM")]
    [SerializeField]
    private AudioMixerGroup m_bgmMixer;
    [SerializeField]
    private BGMList m_bgmList;
    [SerializeField, Range(0.0f, 100.0f)]
    private float m_bgmVolume;

    [Header("SE")]
    [SerializeField]
    private AudioMixerGroup m_seMixer;
    [SerializeField]
    private SEList m_seList;
    [SerializeField, Range(0.0f, 100.0f)]
    private float m_seVolume;

    public BGMList BGMs { get { return m_bgmList; } }
    public SEList SEs { get { return m_seList; } }

    public AudioMixerGroup BGMMixer { get { return m_bgmMixer; } }
    public AudioMixerGroup SEMixer { get { return m_seMixer; } }

    public float BGMVolume
    {
        get
        {
            return m_bgmVolume;
        }
        set
        {
            m_bgmVolume = value;
            if (value < 0.0f) m_bgmVolume = 0.0f;
            if (value > 100.0f) m_bgmVolume = 100.0f;
        }
    }


    public float SeVolume
    {
        get
        {
            return m_seVolume;
        }
        set
        {
            m_seVolume = value;
            if (value < 0.0f) m_seVolume = 0.0f;
            if (value > 100.0f) m_seVolume = 100.0f;
        }
    }
}
