using DataSystem;
using Pool;
using UnityEngine;

namespace SpawningSystem
{
    public class Spawner
    {
        private EnemySpawner m_First_Spawner, m_Second_Spawner;
        private readonly GameConfig m_Game_Config;
        private Player m_Player;
        
        public Spawner(PrefabsDB prefabsDB, Transform parent, PoolManager poolManager, GameConfig gameConfigs)
        {
            m_Game_Config = gameConfigs;
            InstantiateSpawner(prefabsDB.M_Player_Prefab, prefabsDB.M_EnemySpawner_Prefab, parent, poolManager);
        }

        private void InstantiateSpawner(Player player, EnemySpawner enemySpawner, Transform parent, PoolManager poolManager)
        {
            m_First_Spawner = Object.Instantiate(enemySpawner, parent);
            if (m_First_Spawner)
            {
                m_First_Spawner.transform.position = new Vector3(-1, m_Game_Config.M_Spawner_height, 0);
                m_First_Spawner.M_PoolManager = poolManager;
                m_First_Spawner.gameObject.SetActive(false);
            }
            m_Second_Spawner = Object.Instantiate(enemySpawner, parent);
            if (m_Second_Spawner)
            {
                m_Second_Spawner.transform.position = new Vector3(1, m_Game_Config.M_Spawner_height, 0);
                m_Second_Spawner.M_PoolManager = poolManager;
                m_Second_Spawner.gameObject.SetActive(false);
            }
             m_Player = Object.Instantiate(player, parent);
            if (m_Player)
            {
                m_Player.M_PoolManager = poolManager;
                m_Player.M_GameConfig = m_Game_Config;
                m_Player.gameObject.SetActive(false);
            }
            
        }

        public void Run()
        {
            m_First_Spawner.StartRunning();
            m_Second_Spawner.StartRunning();
            m_Player.transform.position = new Vector3(0, m_Game_Config.M_PlayerCurrentYPosition, 0);
            m_Player.transform.rotation = Quaternion.identity;
            m_Player.StartRunning();
        }

        private float LoopValue(float minValue, float maxValue)
        {
            float cycle = Time.time;
            const float tau = Mathf.PI * 2;
            float value = Mathf.Sin(cycle * tau);
            return value * (maxValue - minValue) + minValue;
        }

        public void Move()
        {
            if ((int)(Time.timeSinceLevelLoad / m_Game_Config.M_TimeToChangeFallPattern) % 2 == 0)
            {
                m_First_Spawner.transform.position = new Vector3(LoopValue(-1, -m_Game_Config.M_Max_Side_Ways), m_Game_Config.M_Spawner_height, 0);
                m_Second_Spawner.transform.position = new Vector3(LoopValue(1, m_Game_Config.M_Max_Side_Ways), m_Game_Config.M_Spawner_height, 0);
            }
            else
            {
                m_First_Spawner.transform.position = new Vector3(Random.Range(-m_Game_Config.M_Max_Side_Ways, -1), m_Game_Config.M_Spawner_height, 0);
                m_Second_Spawner.transform.position = new Vector3(Random.Range(1, m_Game_Config.M_Max_Side_Ways), 8, 0);
            }
        }

        public void ChangeInterval()
        {
            m_First_Spawner.M_Spawn_Interval -= Time.deltaTime / 100;
            m_Second_Spawner.M_Spawn_Interval -= Time.deltaTime / 100;
        }

        public void TripleAttack()
        {
            m_Player.StartTripleAttack(m_Game_Config.M_Triple_Attack_Time);
        }

        public void ChangePlayerType()
        {
            m_Player.ChangeType();
        }
    }

}
