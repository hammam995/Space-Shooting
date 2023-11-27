using Pool;
using System.Collections;
using UnityEngine;

namespace SpawningSystem
{
    /// <summary>
    /// Enemy SpawnPoint
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        public float M_Spawn_Interval { get; set; }
        [Header("Parameter")]
        [SerializeField] private float m_total_Spawn_Interval = 0.8f;
      
        //------------------------------------------------------------------------------
        public PoolManager M_PoolManager {private get; set; }
        public void StartRunning()
        {
            M_Spawn_Interval = m_total_Spawn_Interval;
            StartCoroutine(MainCoroutine());
        }

        private IEnumerator MainCoroutine()
        {
            while (true)
            {
                // 5% chance that the falling object is a power up.
                var pool = Random.Range(0, 1f) < .15f ? M_PoolManager.M_Power_Up_Pool : M_PoolManager.M_Enemy_Pool;
                //spawn fallingObject
                FallingObject fallingObject = pool.Request(transform.position);
                fallingObject.M_Pool = pool;
                yield return new WaitForSeconds(M_Spawn_Interval);
            }
        }

        //------------------------------------------------------------------------------

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 1.0f);
        }
    }

}
