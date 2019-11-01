using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField]
    private float m_stageWidth;
    [SerializeField]
    private float m_stageHeight;

    public void OnDrawGizmos()
    {
        Vector3 size = new Vector3(m_stageWidth, m_stageHeight, 1.0f);
        Gizmos.DrawWireCube(Vector3.zero, size);

    }

    public void Start()
    {
        BoxCollider2D[] trigList = GetComponents<BoxCollider2D>();

        trigList[0].size = new Vector2(m_stageWidth, 1.0f);
        trigList[1].size = new Vector2(m_stageWidth, 1.0f);
        trigList[2].size = new Vector2(1.0f, m_stageHeight);
        trigList[3].size = new Vector2(1.0f, m_stageHeight);

        trigList[0].offset = new Vector2(0.0f, m_stageHeight * 0.5f);
        trigList[1].offset = new Vector2(0.0f, -m_stageHeight * 0.5f);
        trigList[2].offset = new Vector2(-m_stageWidth * 0.5f, 0.0f);
        trigList[3].offset = new Vector2(m_stageWidth * 0.5f, 0.0f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            //プレイヤーの死亡ステートを呼び出す
            player.ChangeState<PlayerState_Dead>();
            Destroy(player.gameObject);
            return;
        }

        if (collision.TryGetComponent(out Key key))
        {
            key.Dead();
            return;
        }

        if (collision.TryGetComponent(out MagnetWave mg))
        {
            mg.gameObject.SetActive(false);
        }

    }

}
