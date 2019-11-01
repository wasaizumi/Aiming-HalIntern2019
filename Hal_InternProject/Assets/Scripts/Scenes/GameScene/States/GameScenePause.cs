using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameScenePause : GameSceneState
{
    [SerializeField]
    private AchievementsUI m_pauseUI;
    [SerializeField]
    private string m_returnScene;
    [SerializeField]
    private RectTransform m_cursor;

    private int m_nSelect = 0;
    private float m_waitTime = 0;

    public override void OnStart()
    {
        Time.timeScale = 0;
        SetCursor(m_nSelect);
        m_pauseUI.gameObject.SetActive(true);
        m_pauseUI.SetAhievement(m_scene.m_systemData.CurrentStage);
    }

    public override void OnUpdate()
    {
        SelectUpdate();
        if (m_scene.m_commonInput.Input_Cancel() || m_scene.m_commonInput.Input_Menu())
        {
            SoundObject.Instance.PlayBGM();
            ReturnGame();
        }
        if (m_scene.m_commonInput.Input_Submit())
        {
            if(m_nSelect == 0)
            {
                SoundObject.Instance.PlayBGM();
                ReturnGame();
            }
            if (m_nSelect == 1)
            {
                Time.timeScale = 1;
                SoundObject.Instance.PlayBGM();
                SoundObject.Instance.PlaySE("Decide");
                m_scene.ChangeScene(SceneManager.GetActiveScene().name, 2.0f, false, true);
                m_scene.ChangeState<GameSceneExit>();
            }
            else if (m_nSelect == 2)
            {
                Time.timeScale = 1;
                SoundObject.Instance.PlaySE("Decide");
                m_scene.ChangeScene(m_returnScene, 2.0f, false, false);
                m_scene.ChangeState<GameSceneExit>();
            }
            else if (m_nSelect == 3)
            {
                SoundObject.Instance.PlaySE("Cancel");
                m_scene.ChangeState<GameSceneSetting>();
            }
        }
        m_waitTime += 0.02f;
    }

    public override void OnRelease()
    {
        m_pauseUI.gameObject.SetActive(false);
    }

    private void SelectUpdate()
    {
        if (m_scene.m_commonInput.WaitTime > m_waitTime) return;
        int oldSelect = m_nSelect;
        var list = m_pauseUI.m_selectList;

        float vertical = Input.GetAxisRaw(m_scene.m_commonInput.LVertical);

        if (vertical == 0) return;

        if (InputHandler.IsPositive(vertical)) m_nSelect--;
        if (InputHandler.IsNegative(vertical)) m_nSelect++;

        if (oldSelect == m_nSelect) return;

        m_waitTime = 0f;

        if (m_nSelect < 0) m_nSelect = list.Count - 1;
        if (m_nSelect >= list.Count) m_nSelect = 0;

        SoundObject.Instance.PlaySE("CursorMove");
        SetCursor(m_nSelect);
    }

    private void ReturnGame()
    {
        Time.timeScale = 1;
        m_scene.ChangeState<GameSceneIdle>();
    }

    public void SetCursor(int nSelect)
    {
        Vector3 pos = m_cursor.position;
        pos.y = m_pauseUI.m_selectList[nSelect].position.y;
        m_cursor.position = pos;
    }
}
