using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleSpritePlayer : PoleSprite
{
    [SerializeField]
    private PoleSprites m_sprites;

    public override void PoleChange(PoleObject poleObject)
    {
        base.PoleChange(poleObject);

        SpriteChange(m_sprites, poleObject.m_pole);
    }
    // スプライト反転用
    public void SpriteFlip(bool flag)
    {
        m_spriteRenderer.flipX = flag;
    }
}
