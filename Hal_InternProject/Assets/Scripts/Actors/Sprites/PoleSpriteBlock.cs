using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleSpriteBlock : PoleSprite
{
    [SerializeField]
    private PoleSpriteData m_poleSprites;

    public override void PoleChange(PoleObject poleObject)
    {
        base.PoleChange(poleObject);
        base.SpriteChange(m_poleSprites.GetPoleSprites(), poleObject.m_pole);
    }
}
