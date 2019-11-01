using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementUI : MonoBehaviour
{
    [SerializeField]
    private Image m_symbol;
    [SerializeField]
    private Color m_achiveColor;

    public void AchiveEffect(bool isClear)
    {
        if (isClear)
            ClearAchiveEffect();
        else
            FailedAchiveEffect();
    }

    //達成時の演出
    private void ClearAchiveEffect()
    {
        m_symbol.color = Color.white;
    }

    //失敗時の演出
    private void FailedAchiveEffect()
    {
        m_symbol.color = Color.black;
    }
}
