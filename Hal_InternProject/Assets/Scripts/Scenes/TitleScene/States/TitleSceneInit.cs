using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneInit : TitleSceneState
{
    [SerializeField]
    private GameObject m_titleColum;
    [SerializeField]
    private GameObject m_pushAnyButton;

    private bool m_isInputEnable = false;

    public override void Start()
    {
        SoundObject.Instance.PlayBGM("TitleScene");
        m_titleColum.SetActive(true);
        m_pushAnyButton.SetActive(false);
        StartCoroutine(InputCoroutine(0.1f));
    }

    public override void OnStart()
    {
        
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnRelease()
    {
        m_pushAnyButton.SetActive(false);
    }

    private IEnumerator InputCoroutine(float waitTime)
    {
        while (FadeController.IsActive)
            yield return null;
        yield return new WaitForSeconds(waitTime);
        m_scene.ChangeState<TitleSceneIdle>();
        yield break;
    }
}
