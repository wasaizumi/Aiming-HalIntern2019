using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorUI : MonoBehaviour
{
    [SerializeField]
    private Color m_submitColor = Color.red;
    [SerializeField]
    private Color m_defaultColor = Color.white;
    [SerializeField]
    private List<Image> m_targetImages;

    public void SetSubmit()
    {
        foreach(var image in m_targetImages)
        {
            image.color = m_submitColor;
        }
    }

    public void SetDefault()
    {
        foreach (var image in m_targetImages)
        {
            image.color = m_defaultColor;
        }
    }
}
