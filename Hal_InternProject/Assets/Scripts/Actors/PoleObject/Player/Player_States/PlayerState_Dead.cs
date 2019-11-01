using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Dead : PlayerState
{
    [SerializeField]
    private GameObject m_deadEffect;
    public override void OnStart()
    {
        SoundObject.Instance.PlaySE("Miss");
        m_player.Dead();

        GameObject dead = Instantiate(m_deadEffect);

        dead.transform.position = m_player.gameObject.transform.position;

        CameraShake shake = Camera.main.gameObject.GetComponent<CameraShake>();

        shake.Shake(0.5f, 0.2f);
    }

    public override void OnUpdate()
    {
        // 状態遷移があればこれ以上更新しない
        if (StateTransition())
            return;

    }

    public override void OnFixedUpdate()
    {

    }


    public override void OnRelease()
    {

    }

    protected override bool StateTransition()
    {
        // ToDo::自動遷移の条件はここに書く

        return false;
    }

}
