using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : BaseActor
{
    //このリストでテレポートするオブジェクトを管理する
    [SerializeField, NonEditable]
    private List<BaseActor> m_teleportActorList;

    public Portal m_portalNext;

    public override void OnStart()
    {
        m_teleportActorList = new List<BaseActor>();
    }

    //範囲に入ったら相手のリストを検索して、あったら転送しない
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
            return;

        if(!collision.TryGetComponent<BaseActor>(out BaseActor others))
            others = collision.GetComponentInParent<BaseActor>();

        //範囲内に来たPoleObjectを転送する
        if (others != null && !SearchActor(others))
        {
            SoundObject.Instance.PlaySE("Portal");
            m_portalNext.AddActor(others);

            // 一旦アクティブをオフにして1フレーム後にオンにする
            StartCoroutine(SetActiveObject(others.gameObject));
            others.gameObject.transform.position = m_portalNext.gameObject.transform.position;
        }
    }

    IEnumerator SetActiveObject(GameObject target)
    {
        target.SetActive(false);
        yield return null;
        target.SetActive(true);
    }

    //範囲から離れたらリストから削除
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger)
            return;

        if (!collision.TryGetComponent<BaseActor>(out BaseActor others))
        {
            others = collision.GetComponentInParent<BaseActor>();
        }

        RemoveActor(others);
    }

    public void RemoveActor(BaseActor oldActor)
    {
        if (m_teleportActorList.Find(actor => actor == oldActor))
            m_teleportActorList.Remove(oldActor);
    }

    public void AddActor(BaseActor new_actor)
    {
        m_teleportActorList.Add(new_actor);
    }

    public bool SearchActor(BaseActor target_actor)
    {
        return m_teleportActorList.Exists(actor => actor == target_actor);
    }

}
