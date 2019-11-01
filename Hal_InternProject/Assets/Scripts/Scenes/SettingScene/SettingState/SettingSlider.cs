using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSlider : MonoBehaviour
{

    [SerializeField]
    private Slider m_silder;
    [SerializeField, Range(0.0f, 1.0f)]
    private float m_changeValue = 0.1f;

    private float m_inputSpeed = 0f;
    public float NormalizedValue { get { return m_silder.value / m_silder.maxValue; } }
    public float Value { get { return m_silder.value; } }

    public void Start()
    {
        m_silder.maxValue = 100f;
        m_silder.minValue = 0f;
        m_silder.value = m_silder.maxValue;
        m_silder.wholeNumbers = true;
    }

    //[Value]は[min～max]範囲内の値を入れる
    //範囲を超えるとValueが範囲内にClampされる
    public void SetValue(float value,float min = 0f,float max = 100f)
    {
        m_silder.minValue = min;
        m_silder.maxValue = max;
        m_silder.value = Mathf.Lerp(min,max,value);
    }

    public float ChangeValue(InputHandler inputHandler)
    {
        float horizontal = Input.GetAxisRaw(inputHandler.LHorizontal);

        if (horizontal == 0f) m_inputSpeed = 0f;
        if (InputHandler.IsPositive(horizontal)) m_inputSpeed += m_changeValue;
        if (InputHandler.IsNegative(horizontal)) m_inputSpeed -= m_changeValue;
        Mathf.Clamp(m_inputSpeed, m_silder.minValue, m_silder.maxValue);
        m_silder.value += m_inputSpeed;

        return m_silder.value;
    }
}
