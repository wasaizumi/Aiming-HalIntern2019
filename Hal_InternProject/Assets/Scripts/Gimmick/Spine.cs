using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spine : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.TryGetComponent<Player>(out Player player)) return;

        //プレイヤーの死亡ステートを呼び出す
        player.ChangeState<PlayerState_Dead>();

        Destroy(player.gameObject);

        Debug.Log("Player死亡");

    }
}
