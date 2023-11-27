using DataSystem;
using Pool;
using SpawningSystem;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
/// <summary>
/// Stage main loop
/// </summary>
public class StageLoop : MonoBehaviour
{
    #region static 
    static public StageLoop Instance { get; private set; }
    #endregion

    #region Variables
    public TitleLoop m_title_loop;
    [Header("Text Information")]
    public Transform m_stage_transform;
    public AudioSource M_Audio_Source;
    [Header("Database")]
    public AudioDB M_Audio_DB;
    public PrefabsDB M_Prefab_Db;
    [Header("Game Config")]
    public GameConfig M_Game_Config;
    private AudioController m_audio_Controller;
    private PoolManager m_PoolManager;
    private int TotalEnemies = 15;
    private Coroutine m_Slow_Motion_Routine;
   
    private float m_move_Speed;
    public Action GameOverEvent;
    public event Action<string, Color> ShowUIMessage;
    private Spawner m_Spawner;
    private float m_Temp_Time = 0;
    #endregion

    #region Properties
    public PowerUpController M_Power_Up_Controller { get; private set; }
    public bool M_Game_Over { get; set; } = false;
    public int m_game_score { get; private set; } = 0;
    private bool m_Slow_Motion = false;
    public bool M_Slow_Motion
    {
        set
        {
            m_Slow_Motion = value;
            if (value)
            {
                if (m_Slow_Motion_Routine != null)
                    StopCoroutine(m_Slow_Motion_Routine);
                m_Slow_Motion_Routine = StartCoroutine(ResetSlowMotion());
            }
        }
    }

    public float M_Move_Speed
    {
        get
        {
            return m_Slow_Motion ? m_move_Speed / 2 : m_move_Speed;
        }
    }

    private int m_Current_Enemies;

    public int M_Passed_Enemies
    {
        get
        {
            return m_Current_Enemies;
        }
        set
        {
            m_Current_Enemies = value < 0 ? 0 : value;
            if (m_Current_Enemies == TotalEnemies)
                GameOver();
            else          
                m_title_loop.SetScore(m_Current_Enemies);           
        }
    }
    #endregion

    private void Start()
    {
        Instance = this;
        CreateGamObjectAndController();
    }

    public void ShowMessage(string message, Color color) => ShowUIMessage?.Invoke(message, color);
    private void SetInitialValue()
    {
        m_move_Speed = M_Game_Config.M_InitialFallingObjectSpeed;
        m_game_score = 0;
    }

    private IEnumerator ResetSlowMotion()
    {
        yield return new WaitForSeconds(M_Game_Config.M_SlowMotionTime);
        m_Slow_Motion = false;
    }

    private void CreateGamObjectAndController()
    {
        m_audio_Controller = new AudioController(M_Audio_DB);
        m_PoolManager = new PoolManager(M_Prefab_Db);
        m_Spawner = new(M_Prefab_Db, m_stage_transform, m_PoolManager, M_Game_Config);
        M_Power_Up_Controller = new PowerUpController(this, M_Game_Config, m_Spawner);
    }

    public void StartStageLoop()
    {
        M_Game_Over = false;
        M_Passed_Enemies = 0;
        StartCoroutine(StageCoroutine());
    }

    void SetupStage()
    {
        ResetStage(true);
        SetInitialValue();
        m_title_loop.RefreshScore(m_game_score);
        StartCoroutine(UpdateProperties());
        m_Spawner.Run();
    }
    private void GameOver()
    {
        M_Game_Over = true;
        m_Slow_Motion = false;
        m_Temp_Time = 0;
        GameOverEvent?.Invoke();
        ResetStage(false);
        m_title_loop.StartTitleLoop();
    }

    #region loop
    /// <summary>
    /// stage loop
    /// </summary>
    private IEnumerator StageCoroutine()
    {
        Debug.Log("Start StageCoroutine");

        SetupStage();

        while (!M_Game_Over)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //exit stage
                GameOver();
                yield break;
            }
            m_Spawner.Move();
            yield return null;
        }
    }
    #endregion

    void ResetStage(bool enable)
    {
        //Enable or Disable Base on boolean value
        for (var n = 0; n < m_stage_transform.childCount; ++n)
        {
            Transform temp = m_stage_transform.GetChild(n);
            temp.gameObject.SetActive(enable);
        }
    }

    public void InstantiateHitVFXAndAddScore(int a_value, Vector3 hitPoint)
    {
        var hitVfx = m_PoolManager.M_Hit_VFX_Particle.Request(hitPoint);
        hitVfx.M_Pool = m_PoolManager.M_Hit_VFX_Particle;
        m_game_score += a_value;
        m_title_loop.RefreshScore(m_game_score);
    }

    private IEnumerator UpdateProperties()
    {
        while (!M_Game_Over)
        {
            if(m_move_Speed <= M_Game_Config.M_TotalMaxSpped)
            {
                m_move_Speed += Time.deltaTime / 5;
                m_Spawner.ChangeInterval();
            }
            m_Temp_Time += Time.deltaTime;
            if (m_Temp_Time > M_Game_Config.M_Chage_Type_Time)
            {
                m_Temp_Time = 0;
                m_Spawner.ChangePlayerType();
                ShowMessage("Player Changed", M_Game_Config.m_Player_Change_Color);
            }
            yield return null;
        }
    }

    public void PlayAudio(EAudioTag m_Audio_Tag) => m_audio_Controller.PlayAudio(M_Audio_Source, m_Audio_Tag);

    private void OnValidate()
    {
        Assert.IsNotNull(m_stage_transform, $"{nameof(m_stage_transform)} cannot be null in {name}");
        Assert.IsNotNull(M_Audio_Source, $"{nameof(M_Audio_Source)} cannot be null in {name}");
        Assert.IsNotNull(M_Audio_DB, $"{nameof(M_Audio_DB)} cannot be null in {name}");
        Assert.IsNotNull(M_Game_Config, $"{nameof(M_Game_Config)} cannot be null in {name}");
    }
}
