using DataSystem;

namespace Pool
{
    public class PoolManager
    {
        private IPool<PlayerBullet> m_Player_Bullet_Pool;
        public IPool<PlayerBullet> M_Player_Bullet_Pool
        {
            get
            {
                if(m_Player_Bullet_Pool == null)
                    m_Player_Bullet_Pool = new Pool<PlayerBullet>(prefabsDB.M_PlayerBulletPrefab);
                return m_Player_Bullet_Pool;
            }
        }
        private IPool<FallingObject> m_Power_Up_Pool;
        public IPool<FallingObject> M_Power_Up_Pool
        {
            get
            {
                if (m_Power_Up_Pool == null)
                    m_Power_Up_Pool = new PoolWithMultipleItem<FallingObject>(prefabsDB.M_PowerUp_Prefab);
                return m_Power_Up_Pool;
            }
        }
        private IPool<FallingObject> m_Enemy_Pool;
        public IPool<FallingObject> M_Enemy_Pool
        {
            get
            {
                if (m_Enemy_Pool == null)
                    m_Enemy_Pool = new Pool<FallingObject>(prefabsDB.M_Enemy_Prefab);
                return m_Enemy_Pool;
            }
        }
        private IPool<HitVFX> m_Hit_VFX_Particle;
        public IPool<HitVFX> M_Hit_VFX_Particle
        {
            get
            {
                if (m_Hit_VFX_Particle == null)
                    m_Hit_VFX_Particle = new PoolWithMultipleItem<HitVFX>(prefabsDB.M_Hit_Partical_System);
                return m_Hit_VFX_Particle;
            }
        }

        private readonly PrefabsDB prefabsDB;

        public PoolManager(PrefabsDB prefabsDB)
        {
            this.prefabsDB = prefabsDB;
        }
    }
}
