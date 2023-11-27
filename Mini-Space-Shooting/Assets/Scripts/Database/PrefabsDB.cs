using SpawningSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace DataSystem
{
    [CreateAssetMenu(fileName = "PrefabsDB", menuName = "DB/Prefab", order = 0)]
    public class PrefabsDB : ScriptableObject
    {
        [Header("Add player Prefab")]
        public Player M_Player_Prefab;
        [Header("Add Enemy Sapwner Prefab")]
        public EnemySpawner M_EnemySpawner_Prefab;
        [Header("Add Enemy Prefab")]
        public FallingObject M_Enemy_Prefab;
        [Header("Add Power Up Prefab")]
        public List<FallingObject> M_PowerUp_Prefab;
        [Header("Add Power Up Prefab")]
        public List<HitVFX> M_Hit_Partical_System;
        [Header("Add m_Player Bullet Prefab")]
        public PlayerBullet M_PlayerBulletPrefab;

        private void OnValidate()
        {
            Assert.IsNotNull(M_Player_Prefab, $"{nameof(M_Player_Prefab)} cannot be null in {name}");
            Assert.IsNotNull(M_EnemySpawner_Prefab, $"{nameof(M_EnemySpawner_Prefab)} cannot be null in {name}");
            Assert.IsNotNull(M_PowerUp_Prefab, $"{nameof(M_PowerUp_Prefab)} cannot be null in {name}");
            Assert.IsNotNull(M_PlayerBulletPrefab, $"{nameof(M_PlayerBulletPrefab)} cannot be null in {name}");
        }
    }

}

