using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //コインの取得処理

    [SerializeField]
    private GimmickAnimation m_animData;
    private SpriteRenderer m_sprite;

    public GameObject m_getEffect;

    public event System.Action m_getAction = delegate() { };

    public void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();

    }

    public void Update()
    {
        if(m_sprite)
            m_sprite.sprite = m_animData.GetAnimImage(Mathf.RoundToInt(Time.time / m_animData.Speed) % m_animData.AnimNum);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Player>(out Player player))return;


        //コイン数ゲットメッセージ予定地
        m_getAction();
        SoundObject.Instance.PlaySE("Coin");

        Instantiate(m_getEffect, transform.position, transform.rotation);

        Destroy(this.gameObject);
        
    }
}
