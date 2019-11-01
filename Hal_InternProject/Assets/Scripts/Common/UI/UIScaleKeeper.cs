using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScaleKeeper : MonoBehaviour
{
    [SerializeField]
    private RectTransform m_rectTransform;

    private void Start()
    {
        if (!m_rectTransform)
            m_rectTransform = GetComponent<RectTransform>();

        
    }

    private void Update()
    {
        m_rectTransform.rect.Set(0, 0, Screen.width, Screen.height);
        m_rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}
