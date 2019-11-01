using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ActorController
 * BaseActorに対するコントローラー
 */

public class ActorController : MonoBehaviour
{
    // この中からゲームで使うオブジェクトを抽出する
    [SerializeField]
    private GameObject m_stageobject;
    // アクターリスト
    [SerializeField, NonEditable]
    private List<BaseActor> m_actorList = new List<BaseActor>();

    private void Awake()
    {
        
    }

    // アクターの初期化
    public void OnStart()
    {
        BaseActor[] obj = m_stageobject.GetComponentsInChildren<BaseActor>();

        foreach (var actor in obj)
        {
            actor.Initialize(this);
            actor.OnStart();
            m_actorList.Add(actor);
        }
        Debug.Log("actorCount : " + m_actorList.Count);
    }

    public void OnUpdate()
    {
        foreach (var actor in m_actorList)
        {
            actor.OnUpdate();
        }
    }

    public void OnFixedUpdate()
    {
        foreach (var actor in m_actorList)
        {
            actor.OnFixedUpdate();
        }
    }



    // アクターの破棄
    public void RemoveActor(BaseActor actor)
    {
        m_actorList.Remove(actor);
    }

    public void AddObject(BaseActor new_actor)
    {
        new_actor.Initialize(this);
        new_actor.OnStart();
        m_actorList.Add(new_actor);

    }

}
