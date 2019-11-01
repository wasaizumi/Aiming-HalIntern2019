using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PoleSprites
{
    public Sprite Pole_None;
    public Sprite Pole_N;
    public Sprite Pole_S;
}

public abstract class PoleSprite : MonoBehaviour
{
    protected SpriteRenderer m_spriteRenderer;

    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void PoleChange(PoleObject poleObject)
    {
        if(!m_spriteRenderer)
            m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void SpriteChange(PoleSprites sprites, PoleObject.Pole pole)
    {
        if (pole == PoleObject.Pole.S)
            m_spriteRenderer.sprite = sprites.Pole_S;
        else if (pole == PoleObject.Pole.N)
            m_spriteRenderer.sprite = sprites.Pole_N;
        else
            m_spriteRenderer.sprite = sprites.Pole_None;
    }
}
