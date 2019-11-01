using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="GameData/PoleSpriteData",fileName ="PoleSprits")]
public class PoleSpriteData : ScriptableObject
{
    [SerializeField]
    private PoleSprites m_sprites = new PoleSprites();

    public PoleSprites GetPoleSprites()
    {
        return m_sprites;
    }
}
