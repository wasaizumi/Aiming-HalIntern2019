using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GimmickAnimation", menuName = "GameData/GimmickAnim")]
public class GimmickAnimation : ScriptableObject
{
    [SerializeField, Range(0.01f, 1.0f)]
    private float m_speed;

    [SerializeField]
    private List<Sprite> m_imageList;

    public float Speed { get { return m_speed; } }
    public int AnimNum { get { return m_imageList.Count; }}

    public Sprite GetAnimImage(int index)
    {
        return m_imageList[index];
    }
}

