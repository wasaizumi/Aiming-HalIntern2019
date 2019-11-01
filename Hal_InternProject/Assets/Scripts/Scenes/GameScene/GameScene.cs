using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : SceneController
{
    [Header("GameScene")]
    public InputHandler m_commonInput;
    public GameObject m_stageParent;

    [Space(8)]
    public ActorController m_actorController;
    [SerializeField,NonEditable]//時間
    public float m_currentTime = 0f;
    [SerializeField]//取得コイン数
    public int m_currentCoin = 0;
    [SerializeField]//マグネット切り替え数
    public int m_changeNum = 0;

    [Header("StageObjects")]
    [SerializeField,NonEditable]
    private Player[] m_players;
    [SerializeField,NonEditable]
    private Goal[] m_goals;
    [SerializeField, NonEditable]
    private Coin[] m_coins;
    [SerializeField, NonEditable]
    private Key[] m_keys;
    [SerializeField, NonEditable]
    public List<GameEvent> m_gameEvents;

    protected override void Start()
    {
        CreateStagePrefab();
        m_actorController.OnStart();
        base.Start();
    }

    [ContextMenu("CreatePrefab")]
    private void CreateStagePrefab()
    {
        if (!m_systemData) return;
        if (!m_systemData.CurrentStage.StagePrefab) return;
        GameObject instance = Instantiate<GameObject>(m_systemData.CurrentStage.StagePrefab);
        instance.transform.SetParent(m_stageParent.transform);

        m_players = this.GetComponentsInChildren<Player>();
        foreach(var player in m_players)
        {
            player.m_poleChange += delegate ()
            {
                m_changeNum++;
            };
        }
        m_goals = this.GetComponentsInChildren<Goal>();
        //コイン加算
        m_coins = this.GetComponentsInChildren<Coin>();
        foreach(var coin in m_coins){
            coin.m_getAction += delegate()
            {
                m_currentCoin++;
            };
        }
        m_keys = this.GetComponentsInChildren<Key>();

        m_gameEvents = new List<GameEvent>();
        foreach (var gameevent in this.GetComponentsInChildren<GameEvent>()) 
        {
            m_gameEvents.Add(gameevent);
            gameevent.Initialize(this);
        }
    }

    public bool IsClear()
    {
        bool enable = false;
        foreach(Goal goal in m_goals)
        {
            enable = goal.GetGoalCheck();
            if (!enable) break;
        }

        return enable;
    }

    public bool IsDead()
    {
        foreach (Player player in m_players)
            if (player.CheckDead()) return true;

        foreach (Key key in m_keys)
            if (key.m_isDead) return true;

        return false;
    }

    public void PlayerStop()
    {
        foreach (var player in m_players)
            player.Stop();
    }

    //GameEventの続き

    //ActorControllerを停止
    public void ActorStop()
    {

    }

    //ActorControllerを再生
    public void ActorStart()
    {

    }
}
