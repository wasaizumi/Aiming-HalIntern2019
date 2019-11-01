using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    public Player m_player { get; private set; }

    public virtual void Initialize(Player p) { m_player = p; }

    public virtual void OnStart() { }
    public virtual void OnUpdate() {  }

    public virtual void OnFixedUpdate() { }
    public virtual void OnRelease() { }

    // 自動で状態遷移する条件はここに書く
    protected virtual bool StateTransition() { return false; }
}
