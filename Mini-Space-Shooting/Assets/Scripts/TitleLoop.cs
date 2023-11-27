using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>
/// Title Screen Loop
/// </summary>
public class TitleLoop : MonoBehaviour
{
    [SerializeField] private StageLoop m_stage_Loop;

    [SerializeField, Header("Layout")]
    private Transform m_ui_Title;

    [SerializeField] private Text m_Title_Text;
    [SerializeField] private Text m_SmallText_Text;
    [SerializeField] private UIMessage m_UIMessage;
    [SerializeField] private Text m_stage_totalEnemies_text;
    [SerializeField] private Text m_stage_score_text;


    [Header("Help Desk Game Object")]
    [SerializeField] private GameObject m_Help_Object;

    private GameConfig GameConfig => m_stage_Loop.M_Game_Config;
    //------------------------------------------------------------------------------

    private void Start()
    {
        //default start
        StartTitleLoop();
        m_stage_Loop.GameOverEvent += OnGameOver;
        m_stage_Loop.ShowUIMessage += ShowUIMessage;
    }

    private void ShowUIMessage(string message, Color color)
    {
        m_UIMessage.SetMessage(message, color);
    }
    
    private void OnGameOver()
    {
        SetupTitle();
        m_stage_totalEnemies_text.text = string.Empty;
        m_UIMessage.gameObject.SetActive(false);
        m_Title_Text.text = "Game Over";
        m_SmallText_Text.text = $"Total Score : {m_stage_Loop.m_game_score} \n (Press Space Key To Restart)";
    }

    public void SetScore(int currentEnemies)
    {
        m_stage_totalEnemies_text.color = Color.Lerp(GameConfig.m_Good_Score_Color, GameConfig.m_Bad_Score_Color, currentEnemies / (float)GameConfig.M_Total_Enemies);
        m_stage_totalEnemies_text.text = $"Total Enemies Passed : {currentEnemies} / {GameConfig.M_Total_Enemies}";
    }
    //
    #region loop
    public void StartTitleLoop()
    {
        StartCoroutine(TitleCoroutine());
    }

    /// <summary>
    /// Title loop
    /// </summary>
    private IEnumerator TitleCoroutine()
    {
        Debug.Log($"Start TitleCoroutine");

        SetupTitle();

        //waiting game start
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(m_Help_Object.activeInHierarchy)
                {
                    m_Help_Object.SetActive(false);
                }
                else
                {
                    CleanupTitle();
                    //Start StageLoop
                    m_stage_Loop.StartStageLoop();
                    yield break;
                }
                
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                m_Help_Object.SetActive(true);
            }
            yield return null;
        }
    }
    #endregion

    //
    void SetupTitle()
    {
        m_ui_Title.gameObject.SetActive(true);
    }

    void CleanupTitle()
    {
        m_ui_Title.gameObject.SetActive(false);
    }
    public void RefreshScore(int gameScore)
    {
        if (m_stage_score_text)
        {
            m_stage_score_text.text = $"Score {gameScore:00000}";
        }
    }
    private void OnDestroy()
    {
        m_stage_Loop.GameOverEvent -= OnGameOver;
        m_stage_Loop.ShowUIMessage -= ShowUIMessage;
    }

    private void OnValidate()
    {
        Assert.IsNotNull(m_Help_Object, $"{nameof(m_Help_Object)} cannot be null in {name}");
        Assert.IsNotNull(m_ui_Title, $"{nameof(m_ui_Title)} cannot be null in {name}");
        Assert.IsNotNull(m_Title_Text, $"{nameof(m_Title_Text)} cannot be null in {name}");
        Assert.IsNotNull(m_SmallText_Text, $"{nameof(m_SmallText_Text)} cannot be null in {name}");
        Assert.IsNotNull(m_stage_totalEnemies_text, $"{nameof(m_stage_totalEnemies_text)} cannot be null in {name}");
        Assert.IsNotNull(m_stage_score_text, $"{nameof(m_stage_score_text)} cannot be null in {name}");
    }
}
