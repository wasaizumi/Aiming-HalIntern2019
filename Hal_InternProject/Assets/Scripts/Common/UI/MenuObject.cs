using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuObject :MonoBehaviour
{
    [SerializeField]
    private Color m_submitColor = Color.red;
    [SerializeField]
    private Color m_defaultColor = Color.white;

    [SerializeField]
    private List<Image> m_targets;

    [ContextMenu("SetSubmitColor")]
    public void SetSubmitColor()
    {
        foreach (var image in m_targets)
            image.color = m_submitColor;
    }

    [ContextMenu("SetDefaultColor")]
    public void SetDefaultColor()
    {
        foreach (var image in m_targets)
            image.color = m_defaultColor;
    }
}
