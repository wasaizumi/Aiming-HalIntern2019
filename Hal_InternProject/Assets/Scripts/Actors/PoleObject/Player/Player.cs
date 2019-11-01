using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : PoleObject
{
    [Header("PlayerState")]
    [SerializeField]
    private GameObject m_playerState;
    [SerializeField]
    private PlayerState m_currentState;
    private List<PlayerState> m_stateList;

    public CircleCollider2D m_selfCollider { get; private set; }
    public Animator m_animator { get; private set; }

    [Header("Parameters")]
    [SerializeField]
    private bool m_isDead;
    [SerializeField]
    public event System.Action m_poleChange = delegate() { };

    [Range(0, 20)]
    public float m_rotationSpeed;

    [Space(16)]
    [SerializeField,Tooltip("飛ばすもの")]
    private GameObject m_magnetPrefab;
    private MagnetWave m_magnetWave;
    [SerializeField]
    private GameObject m_cursorPrefab;
    private CursorController m_cursorController;

    [SerializeField]
    public InputHandler m_inputHandler;

    private Vector2 m_armDir = Vector2.right;
    public float m_horisontal { get; private set; }
    public float m_vertical { get; private set; }
    public float m_moveAmount { get; private set; }
    public Vector2 m_moveDir { get; private set; }
    public Vector2 m_gravityDir { get; private set; }
    public RaycastHit2D m_castHit { get; private set; }
    public bool m_onGround { get; private set; }

    [SerializeField]
    private Transform m_playerSprite;
    [SerializeField]
    private Transform m_magnetArmTransform;

    private const float CAST_RAD_ERROR = 0.03f;
    private const float CAST_DIST_ERROR = 0.15f;

    public override void OnStart()
    {
        base.OnStart();

        m_animator = GetComponentInChildren<Animator>();

        // 自分のコライダを取得
        m_selfCollider = gameObject.GetComponent<CircleCollider2D>();
        if(m_selfCollider == null)
            Debug.Log("Player : Missing Collider!");

        // インスペクターにセットされたゲームオブジェクトの子要素からPlayerStateを取得していく
        PlayerState[] playerStates = m_playerState.GetComponentsInChildren<PlayerState>();
        m_stateList = new List<PlayerState>();
        foreach (PlayerState ps in playerStates)
        {
            ps.Initialize(this);
            m_stateList.Add(ps);

        }

        // カーソルコントローラーの生成
        if(!m_cursorController)
        {
            m_cursorPrefab = Instantiate(m_cursorPrefab, Vector3.zero, Quaternion.identity);
            m_cursorController = m_cursorPrefab.GetComponent<CursorController>();
        }
        // マグネット弾の生成
        if (!m_magnetWave)
        {
            // 弾を非表示で生成しておく
            GameObject mag = Instantiate(m_magnetPrefab, Vector3.zero, Quaternion.identity);
            mag.SetActive(false);
            m_magnetWave = mag.GetComponent<MagnetWave>();
            m_magnetWave.SetPlayer(this);
            AddObject(m_magnetWave);
        }

        m_gravityDir = -Vector2.up;

        // 初期状態
        m_currentState = m_stateList[0];
        m_currentState.OnStart();
    }

    public override void OnFixedUpdate()
    {
        m_gravityDir = (m_mixForce == Vector2.zero) ? -Vector2.up: m_mixForce;
        m_gravityDir.Normalize();

        m_currentState.OnFixedUpdate();

        base.OnFixedUpdate();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        m_cursorController.OnUpdate();

        InputUpdate();
        
        // 地面に設置しているかを判断する
        m_onGround = OnGround();
        // 設置している場合は抵抗を上げる
        m_rigid2D.drag = (m_onGround) ? 10.0f : 1.0f;

        m_currentState.OnUpdate();
    }

    private bool OnGround()
    {
        Vector2 circlePos = (Vector2)transform.position + m_selfCollider.offset;
        float circleRadius = m_selfCollider.radius - CAST_RAD_ERROR;
        m_castHit = Physics2D.CircleCast(circlePos, circleRadius, m_gravityDir, CAST_DIST_ERROR);
        bool hit = (m_castHit.collider != null);
        if (m_castHit.collider && !m_castHit.collider.GetComponent<Key>())
        {
            transform.parent = m_castHit.transform;
        }
        else
            transform.parent = null;
        return hit;
    }

    // 移動時のめり込み防止
    public void MoveAction(Vector2 move)
    {
        const float rad_error = 0.8f;
        float circleRadius = m_selfCollider.radius * rad_error;
        float moveValue = move.magnitude + m_selfCollider.radius + (1.0f - rad_error);
        Vector2 moveDir = move.normalized;
        Vector2 dir_error = moveDir * m_selfCollider.radius;
        Vector2 circlePos = (Vector2)transform.position + m_selfCollider.offset - dir_error;
        RaycastHit2D cast = Physics2D.CircleCast(circlePos, circleRadius, moveDir, moveValue);
        
        if (cast.collider)
        {
            float dist = cast.distance - m_selfCollider.radius - (m_selfCollider.radius * (1.0f - rad_error));
            if (dist < 0.0f) dist = 0.0f;
            move = moveDir * dist;
        }

        transform.position = (Vector2)transform.position + move;
    }

    // 磁力の範囲に入った時に呼ばれる
    public override void OnCircleTrigger2D(PoleArea mine, PoleArea other)
    {
        if (m_pole == Pole.None) return;
        if (other.m_poleObject.m_pole == Pole.None) return;

        Vector2 dir = other.transform.position - mine.transform.position;
        float distance = dir.magnitude;
        float border = mine.m_radius + other.m_radius;
        if (border <= 0.0f) return;

        // レイキャストを当たったオブジェクトの方向に飛ばす
        // 何でもいいので最初に衝突したコリジョンの法線の方向に力を加える
        RaycastHit2D[] castHit;
        RaycastHit2D? result = null;
        castHit = Physics2D.RaycastAll(mine.transform.position, dir.normalized, distance);
        foreach (var hit in castHit)
        {
            if (transform == hit.collider.transform.parent) continue;
            if (result.HasValue)
            {
                if (result.Value.distance > hit.distance)
                    result = hit;
            }
            else
            {
                result = hit;
            }
        }
        if (!result.HasValue) return;

        Vector2 forceDir;
        forceDir = (m_pole == other.m_poleObject.m_pole) ?
            result.Value.normal :
            -result.Value.normal;

        Vector2 add = forceDir.normalized * (1.0f - distance / border);
        AddPoleForce(add);
    }

    private void InputUpdate()
    {
        m_horisontal = m_inputHandler.Input_LHorizontal();
        m_vertical = m_inputHandler.Input_LVertical();
        m_moveDir = new Vector2(m_horisontal, m_vertical);
        m_moveDir.Normalize();
        m_moveAmount = Mathf.Clamp01(Mathf.Abs(m_horisontal) + Mathf.Abs(m_vertical));

        bool isInput = false;

        // カーソルを動かした時
        if (m_cursorController.GetIsCursorCheck())
        {
            m_armDir = m_cursorController.transform.position - transform.position;
            isInput = true;
        }

        Vector2 armDir;
        armDir.x = m_inputHandler.Input_RHorizontal();
        armDir.y = m_inputHandler.Input_RVertical();

        // 右スティックを動かした時
        if (armDir.magnitude > 0.0f)
        {
            m_armDir = armDir;
            isInput = true;
        }

        m_armDir.Normalize();
        bool isFrip = (Vector2.Dot(transform.right, m_armDir) >= 0) ? false : true;
        SpriteFlip(isFrip);
        Quaternion to_q = Quaternion.FromToRotation(m_magnetArmTransform.right, (isFrip) ? -m_armDir : m_armDir);
        m_magnetArmTransform.rotation = to_q * m_magnetArmTransform.rotation;
        Vector3 angle = m_magnetArmTransform.localEulerAngles;
        angle.x = angle.y = 0.0f;
        m_magnetArmTransform.localEulerAngles = angle;
    }

    // 呼ばれたら弾を撃つ
    public void Fire()
    {
        if (m_magnetWave.IsActive()) return;

        // 飛ばすもの に対して方向(正規化)を与えて生成する
        float distance = 0.5f;
        Vector2 dir = m_armDir.normalized;
        Vector2 pos = (Vector2)m_magnetArmTransform.position + dir * distance;

        m_magnetWave.transform.position = pos;
        m_magnetWave.SetPlayer(this);
        m_magnetWave.SetFrontVector(dir);
        m_magnetWave.Shot();

        // 弾をアクティブにする
        SoundObject.Instance.PlaySE("Shot");
        m_magnetWave.gameObject.SetActive(true);
    }

    private void SpriteFlip(bool flag)
    {
        Vector3 scale = m_playerSprite.transform.localScale;
        scale.x = (flag) ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        m_playerSprite.transform.localScale = scale;
    }

    public override void PoleChange(Pole pole)
    {
        m_poleChange();
        base.PoleChange(pole);
    }

    public bool ChangeState<Type>()
        where Type : PlayerState
    {
        // リストからタイプを検索
        foreach(PlayerState ps in m_stateList)
        {
            // タイプが一致したらそのStateに変更
            if (ps.GetType() != typeof(Type))
                continue;
            m_currentState.OnRelease();
            m_currentState = ps;

            // 状態初期化
            m_currentState.OnStart();
            return true;

        }

        // 遷移失敗
        return false;
    }

    public bool CheckDead()
    {
        return m_isDead;
    }

    public void Dead()
    {
        m_isDead = true;
    }

    public void Stop()
    {
        m_rigid2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}

