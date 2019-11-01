using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleArea : MonoBehaviour
{
    private CircleCollider2D m_circleTrigger;
    public PoleObject m_poleObject { get; private set; }
    public float m_radius { get; private set; }
    [SerializeField]
    private PoleAreaEffect m_effect;

    private void Start()
    {
        m_circleTrigger = GetComponent<CircleCollider2D>();
    }

    private void FixedUpdate()
    {
        m_radius = m_circleTrigger.radius * transform.localScale.magnitude;
    }

    public void SetPoleObject(PoleObject obj)
    {
        m_poleObject = obj;
        PoleChange();
    }

    public void Gizomos(PoleObject obj)
    {
        m_effect.Gizomos(obj);
    }

    public void PoleChange()
    {
        m_effect.PoleChange(m_poleObject);
    }

    public GameObject GetCreateCircleEffect()
    {
        GameObject effect = m_effect.GetActiveEffect();
        Vector3 scale = effect.transform.lossyScale;
        GameObject obj = Instantiate(effect);
        obj.transform.localScale = scale;
        return obj;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.isTrigger) return;
        PoleArea otherArea = collision.GetComponent<PoleArea>();
        if (!otherArea) return;

        m_poleObject.OnCircleTrigger2D(this, otherArea);
    }
}
