using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    [SerializeField]
    private bool m_isEndEvent = false;

    public GameScene m_scene { get; private set; }
    public bool IsEnd { get { return m_isEndEvent; } }

    public void Initialize(GameScene gameScene)
    {
        m_scene = gameScene;
    }

    public virtual void OnStart() { }

    public virtual void OnUpdate() { }

    public virtual void OnRelease() { }

    public void EventEnd()
    {
        m_isEndEvent = true;
    }
}
