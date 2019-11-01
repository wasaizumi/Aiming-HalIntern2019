using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StageSelectSceneIdle : StageSelectSceneState
{
    [SerializeField,Tooltip("キャンセルで戻るScene")]
    private string m_returnScene;

    [Header("UI")]
    [SerializeField]
    private RectTransform m_cursor;
    [SerializeField]
    private RectTransform m_ColumParent;
    [SerializeField]
    private Scrollbar m_scrollbar;
    [SerializeField,Tooltip("選択時のサイズ拡大倍率")]
    private float m_selectScale = 1.5f;

    [SerializeField]
    private int m_nSelect = 0;
    private int m_stageNum = 0;
    private float m_inputTime = 0f;
    private List<GameObject> m_stageColumObjects = new List<GameObject>();

    private Rect m_stageColumRect;

    public override void OnStart()
    {
        SoundObject.Instance.PlayBGM("StageSelect");
        //現在の選択番号を取得
        m_nSelect = m_scene.m_systemData.StageSelectNum;

        //Stage取得
        foreach(Transform child in m_ColumParent)
            m_stageColumObjects.Add(child.gameObject);
        m_stageColumRect = m_stageColumObjects[0].GetComponent<RectTransform>().rect;
        m_stageNum = m_stageColumObjects.Count;

        //カーソル表示
        m_cursor.gameObject.SetActive(true);
        SetCursorPos();
    }

    public override void OnUpdate()
    {
        //決定
        if (m_scene.m_comonInput.Input_Submit())
        {
            this.m_scene.m_systemData.SetStageData(m_nSelect);
            if (!this.m_scene.m_systemData.CurrentStage.IsLock)
            {
                SoundObject.Instance.PlaySE("StageIn");
                m_scene.ChangeState<StageSelectSceneSubmit>();
            }
            else
            {
                SoundObject.Instance.PlaySE("Cancel");
            }
        }

        //キャンセル
        if (m_scene.m_comonInput.Input_Cancel())
        {
            SoundObject.Instance.PlaySE("Cancel");
            m_scene.ChangeScene(m_returnScene, 2f,false,false);
            m_scene.ChangeState<StageSelectExit>();
        }   

        if (m_inputTime > m_scene.m_comonInput.WaitTime)
            MoveCursor();
        m_inputTime += Time.deltaTime;
    }

    public override void OnRelease()
    {

    }

    //カーソル移動
    private void MoveCursor()
    {
        int oldSelect = m_nSelect;

        float vertical = Input.GetAxis(m_scene.m_comonInput.Vertical);

        if (InputHandler.IsPositive(vertical)) m_nSelect--;
        if (InputHandler.IsNegative(vertical)) m_nSelect++;

        //Timeリセット
        if (oldSelect == m_nSelect) return;
        m_inputTime = 0f;

        if (m_nSelect < 0) m_nSelect = oldSelect;
        if (m_nSelect >= m_stageNum) m_nSelect = oldSelect;

        SoundObject.Instance.PlaySE("CursorMove");

        m_scrollbar.value = m_nSelect == 0 ? 0f : (float)m_nSelect / (float)(m_stageNum - 1);
        m_stageColumObjects[oldSelect].transform.localScale = Vector3.one;
        SetCursorPos();
    }

    private void SetCursorPos()
    {
        if (m_nSelect != 0 && m_nSelect != m_stageNum - 1)
        {
            Vector3 pos = m_ColumParent.localPosition;
            pos.y = m_stageColumRect.height * (m_nSelect - 1);
            m_ColumParent.localPosition = pos;
        }
        else
        {
            if (m_nSelect == 0)
                m_ColumParent.localPosition = Vector3.zero;
            else
            {
                Vector3 pos = m_ColumParent.localPosition;
                pos.y = m_stageColumRect.height * (m_stageNum - 2);
                m_ColumParent.localPosition = pos;
            }
        }
        m_stageColumObjects[m_nSelect].transform.localScale = Vector3.one * m_selectScale;
        m_cursor.position = m_stageColumObjects[m_nSelect].transform.position;
    }

    public void ScrollbarMethod()
    {
        int oldSelect = m_nSelect;
        
        m_nSelect = (int)(m_scrollbar.value * (m_stageNum - 1));

        m_stageColumObjects[oldSelect].transform.localScale = Vector3.one;
        SetCursorPos();
    }
}
