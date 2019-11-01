using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SEData
{
    public string m_name;
    public AudioClip m_clip;
    [Range(0,100)]
    public int m_volume;

}

[CreateAssetMenu(fileName = "SEList", menuName = "Sound/SEList")]
public class SEList : ScriptableObject
{
    [SerializeField]
    private List<SEData> m_list;

    public SEData GetByKey(string Key)
    {
        foreach(SEData audio in m_list)
        {
            if(audio.m_name == Key)
                return audio;
        }

        return null;
    }
}
