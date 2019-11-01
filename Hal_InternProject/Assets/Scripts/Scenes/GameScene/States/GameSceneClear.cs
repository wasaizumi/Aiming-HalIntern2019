using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameSceneClear : GameSceneState
{
    [SerializeField]
    private string m_nextSceneName;

    [Header("UI")]
    [SerializeField, Tooltip("クリア時のUI")]
    private ClearUI m_clearUI;
    [SerializeField]
    private Sprite m_cursor;

    [SerializeField]
    private Image[] m_selectColum;

    private float m_ahiveInterval = 1f;
    private int m_nSelect = 0;
    private int m_maxNum = 0;
    private float m_waitTime = 0f;
    private bool m_isInput = false;

    private Sprite m_defaultSelectSprite;

    public override void OnStart()
    {
        m_nSelect = 0;
        m_maxNum = m_selectColum.Length;
        m_defaultSelectSprite = m_selectColum[0].sprite;
        SetCursor();

        SoundObject.Instance.StopBGM(1.0f);
        SoundObject.Instance.PlaySE("Clear");
        StartCoroutine(AchivementCorutine());
    }

    public override void OnUpdate()
    {
        if (m_scene.m_commonInput.WaitTime <= m_waitTime)
            ChangeSelect();

        if (m_scene.m_commonInput.Input_Submit())
        {
            //NextStage
            if(m_nSelect == 0)
            {
                SoundObject.Instance.PlaySE("StageIn");
                NextStage();
            }
            //ReTryStage
            else if (m_nSelect == 1)
            {
                SoundObject.Instance.PlaySE("StageIn");
                RetryStage();
                
            }
            //ReturnScene
            else if (m_nSelect == 2)
            {
                SoundObject.Instance.PlaySE("Cancel");
                NextScene();
            }
        }

        m_waitTime += 0.02f;
    }

    private void ChangeSelect()
    {
        if (!m_isInput) return; 
        int old = m_nSelect;

        float horizontal = m_scene.m_commonInput.Input_LHorizontal();
        if (InputHandler.IsPositive(horizontal)) m_nSelect--;
        if (InputHandler.IsNegative(horizontal)) m_nSelect++;

        if (old == m_nSelect) return;

        if (m_nSelect < 0) m_nSelect = m_maxNum - 1;
        if (m_nSelect >= m_maxNum) m_nSelect = 0;

        SoundObject.Instance.PlaySE("CursorMove");
        m_selectColum[old].sprite = m_defaultSelectSprite;
        SetCursor();
        m_waitTime = 0f;
    }

    private void SetCursor()
    {
        m_selectColum[m_nSelect].sprite = m_cursor;
    }

    public void NextScene()
    {
        m_scene.ChangeScene(m_nextSceneName, 2f, false, false);
        m_scene.ChangeState<GameSceneExit>();
    }

    public void RetryStage()
    {
        m_scene.ChangeScene(SceneManager.GetActiveScene().name,2f,false,true);
        m_scene.ChangeState<GameSceneExit>();
    }
    
    public void NextStage()
    {
        //次のステージへ
        if (m_scene.m_systemData.ChangeNextStage()){
            m_scene.ChangeScene(SceneManager.GetActiveScene().name, 2f,false,true);
            m_scene.ChangeState<GameSceneExit>();
            return;
        }
        //NextSceneへ遷移
        m_scene.ChangeScene(m_nextSceneName, 2f,false,false);
        m_scene.ChangeState<GameSceneExit>();
    }

    //Achivement演出
    IEnumerator AchivementCorutine()
    {
        GameData currentData = m_scene.m_systemData.CurrentStage;
        bool IsAhieve1 = false;
        bool IsAhieve2 = false;
        bool IsAhieve3 = false;

        m_clearUI.m_ahievementsUI.SetAhievement(currentData);

        //Achivement条件群
        {
            //コインを三枚以上
            if (m_scene.m_currentCoin >= currentData.AchieveCoinNum)
            {
                IsAhieve1 = true;
                currentData.IsAchievement1 = true;
            }

            //時間内
            if (m_scene.m_currentTime <= currentData.AchieveLimitTime)
            {
                IsAhieve2 = true;
                currentData.IsAchievement2 = true;
            }

            //ノーミス
            if (m_scene.m_changeNum <= currentData.AchieveChangeNum)
            {
                IsAhieve3 = true;
                currentData.IsAchievement3 = true;
            }

            //全部達成
            if (IsAhieve1 && IsAhieve2 && IsAhieve3)
            {
                //自己ベスト更新
                if (currentData.Time >= m_scene.m_currentTime)
                    currentData.SetTime(m_scene.m_currentTime);
            }

            m_clearUI.SetBestTimeText(currentData.Time);
        }

        currentData.IsClear();
        //次ステージのアンロック
        m_scene.m_systemData.UnLockNestStage();
        m_clearUI.SetTimeText(m_scene.m_currentTime);
        m_clearUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(m_ahiveInterval);
        m_isInput = true;

        //Achivement解除演出
        {
            SoundObject.Instance.PlaySE("Coin");
            m_clearUI.m_achievement_1.AchiveEffect(IsAhieve1);
            yield return new WaitForSeconds(m_ahiveInterval);

            m_clearUI.m_achievement_2.AchiveEffect(IsAhieve2);
            SoundObject.Instance.PlaySE("Coin");
            yield return new WaitForSeconds(m_ahiveInterval);

            m_clearUI.m_achievement_3.AchiveEffect(IsAhieve3);
            SoundObject.Instance.PlaySE("Coin");
            yield return new WaitForSeconds(m_ahiveInterval);
        }

        yield break;
    }
}
