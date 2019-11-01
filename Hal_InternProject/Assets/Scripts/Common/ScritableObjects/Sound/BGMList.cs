using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class BGMData
{
    public string m_name;
    public AudioClip m_clip;
    [Range(0,100)]
    public int m_volume;

    public int m_loopBigin;
    public int m_loopEnd;

}

[CreateAssetMenu(fileName = "BGMList", menuName = "Sound/BGMList")]
public class BGMList : ScriptableObject
{
    [SerializeField]
    private List<BGMData> m_list;

    public BGMData GetByKey(string Key)
    {
        foreach(BGMData audio in m_list)
        {
            if(audio.m_name == Key)
                return audio;
        }

        return null;
    }
}
