using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSwap : MonoBehaviour
{
    private PoleObject m_player;
    private PoleObject m_target;

    private Vector3 m_playerScale;
    private Vector3 m_targetScale;

    private GameObject[] m_swapObject = new GameObject[2];
    private float m_swapTime = 0.0f;
    [SerializeField]
    private float m_swapSpeed = 0.0f;

    public void OnStart()
    {
        m_swapObject[0] = SpawnSwapEffect(m_player);
        m_swapObject[1] = SpawnSwapEffect(m_target);

        m_playerScale = m_swapObject[0].transform.lossyScale;
        m_targetScale = m_swapObject[1].transform.lossyScale;

        m_swapTime = 0.0f;
    }

    public void OnUpdate()
    {
        m_swapObject[0].transform.position = SwapLerp(m_swapObject[0], m_target, m_swapTime);
        m_swapObject[1].transform.position = SwapLerp(m_swapObject[1], m_player, m_swapTime);

        m_swapObject[0].transform.localScale = Vector3.Lerp(m_swapObject[0].transform.localScale, m_targetScale, m_swapTime);
        m_swapObject[1].transform.localScale = Vector3.Lerp(m_swapObject[1].transform.localScale, m_playerScale, m_swapTime);

        m_swapTime += Time.deltaTime * m_swapSpeed;

        if (m_swapTime >= 0.5f)
        {
            PoleSwap(m_player, m_target);

            SwapFinish();
        }
    }
    public void SetSwapObject(PoleObject player, PoleObject target)
    {
        m_player = player;
        m_target = target;
    }

    private void PoleSwap(PoleObject player, PoleObject other)
    {
        PoleObject.Pole tmp = other.m_pole;
        other.PoleChange(player.m_pole);
        player.PoleChange(tmp);
    }

    private Vector2 SwapLerp(GameObject flyingobj, PoleObject target, float t)
    {
        Vector2 target_pos = flyingobj.transform.position;

        if (target)
            target_pos = target.transform.position;

        return Vector2.Lerp(flyingobj.transform.position, target_pos, t);
    }

    public GameObject SpawnSwapEffect(PoleObject obj)
    {
        if (!obj)
            return null;

        GameObject swapeffect = obj.GetCreateCircleEffect();
        swapeffect.transform.position = obj.transform.position;
        return swapeffect;
    }

    private void SwapFinish()
    {
        this.gameObject.SetActive(false);
        Destroy(m_swapObject[0]);
        Destroy(m_swapObject[1]);
    }
}
