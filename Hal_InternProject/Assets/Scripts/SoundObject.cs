using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    public string m_bgmInitKey;

    [SerializeField]
    private SoundGeneral m_param;
    public SoundGeneral Param { get { return m_param; }}

    [SerializeField]
    private AudioSource m_bgmSource;

    private BGMData m_data;

    private float m_fadeoutRatio;
    private static SoundObject m_soundObject;

    public static SoundObject Instance
    {
        get
        {
            if (m_soundObject == null)
            {
                GameObject obj = Instantiate(Resources.Load("Prefab/Scene/SoundManager")) as GameObject;
                m_soundObject = obj.GetComponent<SoundObject>();
                DontDestroyOnLoad(obj);
            }
            return m_soundObject;
        }
    }

    public void Start()
    {
        Initialize();
        PlayBGM(m_bgmInitKey);
    }

    public void Update()
    {
        m_param.BGMMixer.audioMixer.SetFloat("Volume_bgm", m_param.BGMVolume - 80.0f);
        m_param.SEMixer.audioMixer.SetFloat("Volume_se", m_param.SeVolume - 80.0f);

        if (m_bgmSource.volume > 0)
        {
            // ループチェック
            if (m_data.m_loopEnd > 0 && m_bgmSource.timeSamples > m_data.m_loopEnd)
                m_bgmSource.timeSamples = m_data.m_loopBigin;

            // フェードチェック
            if (m_fadeoutRatio > 0)
                m_bgmSource.volume -= m_fadeoutRatio * Time.deltaTime;
        }
        else
            m_bgmSource.Stop();
    }


    public void Reset()
    {
        m_bgmSource = GetComponent<AudioSource>();
        if(m_bgmSource == null)
            m_bgmSource = gameObject.AddComponent<AudioSource>();

        Initialize();
    }

    public void Initialize()
    {
        if (m_bgmSource)
        {
            m_bgmSource.clip = null;
            m_bgmSource.volume = 0.0f;
            m_bgmSource.loop = true;
            m_bgmSource.playOnAwake = true;
            m_bgmSource.spatialBlend = 0;
            m_bgmSource.mute = false;
            m_bgmSource.outputAudioMixerGroup = m_param.BGMMixer;

        }
    }

    // 音を切り替えずに再生
    public void PlayBGM(float delay = 0.0f)
    {
        m_bgmSource.volume = (float)m_data.m_volume * 0.01f;
        m_fadeoutRatio = 0.0f;
        m_bgmSource.PlayDelayed(delay);
    }

    // 音を切り替えて再生
    public void PlayBGM(string Key, float delay = 0.0f)
    {
        m_data = m_param.BGMs.GetByKey(Key);
        if (m_data == null)
        {
            Debug.LogAssertion(Key + " is NotFound");
            return;
        }

        if (m_bgmSource.clip != m_data.m_clip)
            m_bgmSource.clip = m_data.m_clip;

        PlayBGM(delay);
    }

    public void StopBGM()
    {
        if (m_bgmSource.isPlaying)
            m_bgmSource.Stop();
    }

    public void StopBGM(float sec)
    {
        if (m_bgmSource.isPlaying)
        {
            if (sec > 0)
                m_fadeoutRatio = m_bgmSource.volume / sec;
            else
                m_bgmSource.volume = 0;
        }
    }

    public void PauseBGM()
    {
        m_bgmSource.Pause();

    }

    public void PlaySE(string Key)
    {
        // Audio.PlayClipAtPoint(clip, 場所, 音量)を真似て作成
        SEData data = m_param.SEs.GetByKey(Key);
        if (data == null)
        {
            Debug.LogAssertion(Key + " is NotFound");
            return;
        }

        GameObject voice = new GameObject("VoiceObject");
        voice.transform.position = Camera.main.transform.position + new Vector3(0.0f, 0.0f, 5.0f);

        AudioSource seSource = voice.AddComponent<AudioSource>();
        seSource.clip = data.m_clip;
        seSource.volume = (float)data.m_volume / 100.0f;
        seSource.loop = false;
        seSource.playOnAwake = false;
        seSource.spatialBlend = 0;
        seSource.mute = false;
        seSource.outputAudioMixerGroup = m_param.SEMixer;

        seSource.Play();

        voice.transform.parent = transform;
        Destroy(voice, data.m_clip.length);
    }


}
