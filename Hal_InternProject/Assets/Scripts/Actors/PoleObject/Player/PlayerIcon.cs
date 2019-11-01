using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcon : MonoBehaviour
{
    private enum PlayerIndex
    {
        P1,
        P2
    }

    [SerializeField]
    private Sprite[] m_playerIcon = new Sprite[(int)PlayerIndex.P2+1];

    private SpriteRenderer m_spriterenderer;


    [SerializeField]
    private PlayerIndex m_playerIndex;
    // Start is called before the first frame update
    public void OnDrawGizmos()
    {
        if (!m_spriterenderer)
            m_spriterenderer = this.gameObject.GetComponent<SpriteRenderer>();

        m_spriterenderer.sprite = m_playerIcon[(int)m_playerIndex];

    }
}
