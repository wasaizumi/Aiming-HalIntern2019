using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ISceneState
{
    void OnStart();
    void OnUpdate();
    void OnFixedUpdate();
    void OnRelease();
}

[System.Serializable]
public abstract class BaseSceneState : MonoBehaviour, ISceneState
{
    public abstract void Initialize(SceneController scene);

    public virtual void Start() { }
    public virtual void OnStart() { }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
    public virtual void OnRelease() { }
}
