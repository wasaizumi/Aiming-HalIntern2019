using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Input/InputHandler",fileName ="InputHandler")]
public class InputHandler : ScriptableObject
{
    static private float m_positive = 0.1f;
    static private float m_negative = -0.1f;

    [Header("InputParameter")]
    [SerializeField,Tooltip("入力待機")]
    private float m_waitTime = 0.1f;

    [Header("Button")]
    [SerializeField,Tooltip("決定")]
    private string m_submit = "Submit";
    [SerializeField, Tooltip("戻る")]
    private string m_cancel = "Cancel";
    [SerializeField, Tooltip("メニュー")]
    private string m_menu = "Menu";
    [SerializeField,Tooltip("撃つ")]
    private string m_shot = "Fire1";

    [Header("Axis")]
    [SerializeField,Tooltip("水平方向")]
    private string m_RHorizontal = "Horizontal";
    [SerializeField,Tooltip("垂直方向")]
    private string m_RVertical = "Vertical";
    [SerializeField, Tooltip("十字水平方向")]
    private string m_horizontal_Cross = "Horizontal_Cross";
    [SerializeField,Tooltip("十字垂直方向")]
    private string m_vertical_Cross = "Vertical_Cross";
    [SerializeField, Tooltip("右水平方向")]
    private string m_LHorizontal = "Horizontal";
    [SerializeField, Tooltip("右水平方向")]
    private string m_LVertical = "Vertical";
    //プロパティ群

    public float WaitTime
    {
        get { return m_waitTime; }
    }

    public string Submit
    {
        get { return m_submit; }
    }
    public string Cancel
    {
        get { return m_cancel; }
    }
    public string Horizontal_Cross
    {
        get { return m_horizontal_Cross; }
    }
    public string Vertical_Cross
    {
        get { return m_vertical_Cross; }
    }
    public string Horizontal
    {
        get { return m_RHorizontal; }
    }
    public string Vertical
    {
        get { return m_RVertical; }
    }

    public string LVertical
    {
        get { return m_LVertical; }
    }

    public string LHorizontal
    {
        get { return m_LHorizontal; }
    }

    public string Shot
    {
        get { return m_shot; }
    }

    //入力関数群

    public float Input_RHorizontal()
    {
        return Input.GetAxis(m_RHorizontal);
    }

    public float Input_RVertical()
    {
        return Input.GetAxis(m_RVertical);
    }

    public float Input_LHorizontal()
    {
        return Input.GetAxis(m_LHorizontal);
    }

    public float Input_LVertical()
    {
        return Input.GetAxis(m_LVertical);
    }

    public float Input_Horizontal_Cross()
    {
        return Input.GetAxis(m_horizontal_Cross);
    }

    public float Input_Vertical_Cross()
    {
        return Input.GetAxis(m_vertical_Cross);
    }

    public bool Input_Submit()
    {
        return Input.GetButtonUp(m_submit);
    }

    public bool Input_Cancel()
    {
        return Input.GetButtonUp(m_cancel);
    }

    public bool Input_Shot()
    {
        return Input.GetButtonUp(m_shot);
    }

    public bool Input_Menu()
    {
        return Input.GetButtonUp(m_menu);
    }

    static public bool IsPositive(float input)
    {
        return input >= m_positive;
    }

    static public bool IsNegative(float input)
    {
        return input <= m_negative;
    }
}
