using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleSceneIdle : TitleSceneState
{
    [SerializeField]
    private RectTransform m_cursor;

    [Header("Select")]
    [SerializeField]
    private GameObject m_selectColum;
    [SerializeField]
    private GameObject m_selectMenu;
    private TextMeshProUGUI[] m_selectTexts;
    private buttonHundle[] m_selectBtns;

    [Header("WallChara")]
    [SerializeField]
    private GameObject m_selectWallChara;
    [SerializeField]
    private GameObject m_selectWallChara1P;
    [SerializeField]
    private GameObject m_selectWallChara2P;

    [Header("Select")]
    [SerializeField]
    private GameObject m_button_01;
    [SerializeField]
    private GameObject m_button_01_S;
    [SerializeField]
    private GameObject m_button_02;
    [SerializeField]
    private GameObject m_button_02_S;
    [SerializeField]
    private GameObject m_button_03;
    [SerializeField]
    private GameObject m_button_03_S;
    [SerializeField]
    private GameObject m_button_04;
    [SerializeField]
    private GameObject m_button_04_S;

    private int m_nSelect = 0;
    private float m_inputTime = 0f;
    public override void OnStart()
    {
        m_cursor.gameObject.SetActive(true);
        m_selectColum.SetActive(true);
        m_selectTexts = m_selectMenu.GetComponentsInChildren<TextMeshProUGUI>();
        m_selectBtns = m_selectMenu.GetComponentsInChildren<buttonHundle>();

    }

    

    public override void OnUpdate()
    {
        if (m_inputTime >= m_scene.m_commonInput.WaitTime) Select();

        if (Input.GetButtonUp(m_scene.m_commonInput.Submit))
        {
            SoundObject.Instance.PlaySE("Decide");
            if (m_nSelect == 0)
                SelectSinglePlay();
            else if (m_nSelect == 1)
                SelectMultiPlay();
            else if (m_nSelect == 2)
                SelectSetting();
            else if (m_nSelect == 3)
                SelectExit();
        }


        // 壁の画像を切り替える
        if (m_nSelect == 0) { 
            m_selectWallChara.SetActive(false);
            m_selectWallChara1P.SetActive(true);
            m_selectWallChara2P.SetActive(false);

            m_button_01.SetActive(false);
            m_button_01_S.SetActive(true);
            m_button_02.SetActive(true);
            m_button_02_S.SetActive(false);
            m_button_03.SetActive(true);
            m_button_03_S.SetActive(false);
            m_button_04.SetActive(true);
            m_button_04_S.SetActive(false);
        }
        else if (m_nSelect == 1)
        {
            m_selectWallChara.SetActive(false);
            m_selectWallChara1P.SetActive(false);
            m_selectWallChara2P.SetActive(true);

            m_button_01.SetActive(true);
            m_button_01_S.SetActive(false);
            m_button_02.SetActive(false);
            m_button_02_S.SetActive(true);
            m_button_03.SetActive(true);
            m_button_03_S.SetActive(false);
            m_button_04.SetActive(true);
            m_button_04_S.SetActive(false);
        }
        else if (m_nSelect == 2)
        {
            m_selectWallChara.SetActive(true);
            m_selectWallChara1P.SetActive(false);
            m_selectWallChara2P.SetActive(false);

            m_button_01.SetActive(true);
            m_button_01_S.SetActive(false);
            m_button_02.SetActive(true);
            m_button_02_S.SetActive(false);
            m_button_03.SetActive(false);
            m_button_03_S.SetActive(true);
            m_button_04.SetActive(true);
            m_button_04_S.SetActive(false);
        }
        else if (m_nSelect == 3)
        {
            m_selectWallChara.SetActive(true);
            m_selectWallChara1P.SetActive(false);
            m_selectWallChara2P.SetActive(false);

            m_button_01.SetActive(true);
            m_button_01_S.SetActive(false);
            m_button_02.SetActive(true);
            m_button_02_S.SetActive(false);
            m_button_03.SetActive(true);
            m_button_03_S.SetActive(false);
            m_button_04.SetActive(false);
            m_button_04_S.SetActive(true);
        }

        //裏コード
        if (Input.GetKeyUp(KeyCode.Escape))
            StartCoroutine(BackCode());

        m_inputTime += Time.deltaTime;
    }

    public override void OnRelease()
    {
        m_selectColum.SetActive(false);
        m_cursor.gameObject.SetActive(false);
    }

    private void Select()
    {
        int oldSelect = m_nSelect;
        float vertical = m_scene.m_commonInput.Input_LVertical();

        if (InputHandler.IsPositive(vertical)) m_nSelect--;
        if (InputHandler.IsNegative(vertical)) m_nSelect++;

        if (oldSelect == m_nSelect) return;

        SoundObject.Instance.PlaySE("CursorMove");

        if (m_nSelect < 0) m_nSelect = m_selectTexts.Length-1;
        if (m_nSelect >= m_selectTexts.Length) m_nSelect = 0;
        SetCursor();
        m_inputTime = 0f;
    }

    private void SetCursor()
    {
        Vector3 pos = m_cursor.position;
        pos.y = m_selectTexts[m_nSelect].rectTransform.position.y;
        m_cursor.position = pos;
    }

    public void SelectSinglePlay()
    {
        m_nSelect = 0;
        SetCursor();
        m_scene.ChangeState<TitleSceneSinglePlay>();
    }

    public void SelectMultiPlay()
    {
        m_nSelect = 1;
        SetCursor();
        m_scene.ChangeState<TitleSceneMultiPlay>();
    }

    public void SelectSetting()
    {
        m_nSelect = 2;
        SetCursor();
        m_scene.ChangeState<TitleSceneSetting>();
    }

    public void SelectExit()
    {
        m_nSelect = 3;
        SetCursor();
        m_scene.ChangeState<TitleSceneExit>();
    }

    private IEnumerator BackCode()
    {
        float time = 0;
        Debug.Log("裏コード開始");
        while (!Input.anyKeyDown)
        {
            Debug.Log("入力認識");
            time += Time.deltaTime;
            if (time >= 3f) yield break;
            yield return null;
        };
        if (!Input.GetKeyDown(KeyCode.Q)) yield break;
        while (!Input.GetKeyUp(KeyCode.Q))
        {
            Debug.Log("裏コード待機...");
            time += Time.deltaTime;
            yield return null;
        }

        if (time >= 3f)
        {
            m_scene.m_systemData.AllUnLock();
            Debug.Log("UnLockAll");
        }
        yield break;
    }
}