using SpawningSystem;
using UnityEngine;

public class PowerUpController 
{
    private const int m_Min_Total_Reduction = 3;
    private const int m_Max_Total_Reduction = 6;

    private StageLoop e_stage_Loop;

    private readonly GameConfig m_GameConfig;
    private readonly Spawner m_Spawner;
    public PowerUpController(StageLoop stageLoop, GameConfig gameConfig, Spawner spawner)
    {
        e_stage_Loop = stageLoop;
        m_GameConfig = gameConfig;
        m_Spawner = spawner;
    }
    private int SetTotal() => Random.Range(m_Min_Total_Reduction, m_Max_Total_Reduction);

    public void OnPowerUp(EPowerUp ePowerUp)
    {
        switch (ePowerUp)
        {
            case EPowerUp.KillPassedEnemy:
                int kills = SetTotal();
                e_stage_Loop.M_Passed_Enemies -= kills;
                e_stage_Loop.ShowMessage($"Killed {kills} Passed Enemies", m_GameConfig.m_Kill_Enemy_Color);
                break;
            case EPowerUp.TripleBullet:
                e_stage_Loop.ShowMessage("Triple Bullet", m_GameConfig.m_Triple_Shot_Color);
                m_Spawner.TripleAttack();
                break;
            case EPowerUp.SlowMotion:
                e_stage_Loop.ShowMessage("Slow Motion", m_GameConfig.m_Slow_Motion_Color);
                e_stage_Loop.M_Slow_Motion = true;
                break;
        }
    }

}

public enum EPowerUp
{
    KillPassedEnemy,
    TripleBullet,
    SlowMotion
}
