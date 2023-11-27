using Pool;
using System.Collections;
using UnityEngine;

/// <summary>
/// m_Player Character
/// </summary>
public class Player : MonoBehaviour
{
    public float m_move_speed => M_GameConfig.M_PlayerMoveSpeed;

    private bool m_Is_Triple_Shot = false;

    private Coroutine tripleShotCoroutine;
    private EPlayerType m_PlayerType;
    private float temp_Time = 0f;
    private float m_Attack_Time_Interval => M_GameConfig.M_Attack_Time_Interval;

    private int m_playerType_int = 0;

    private float M_Total_Time
    {
        get
        {
            return m_PlayerType switch
            {
                EPlayerType.SingleCharge => m_Attack_Time_Interval,
                EPlayerType.DoubleBullet => m_Attack_Time_Interval * 2,
                EPlayerType.RoundRobin => m_Attack_Time_Interval * 3,
                _ => m_Attack_Time_Interval,
            };
        }
    }
   
    public GameConfig M_GameConfig
    {
        private get;
        set;
    }

    public PoolManager M_PoolManager
    {
        private get;
        set;
    }

    public void StartTripleAttack(float time)
    {
        tripleShotCoroutine = StartCoroutine(SetTripleAttack(time));
    }

    private IEnumerator SetTripleAttack(float time)
    {
        m_Is_Triple_Shot = true;
        yield return new WaitForSeconds(time);
        m_Is_Triple_Shot = false;
    }

    public void StartRunning()
    {
        m_Is_Triple_Shot = false;
        m_playerType_int = 2;
        ChangeType();
        StartCoroutine(MainCoroutine());
    }

    //
    private IEnumerator MainCoroutine()
    {
        while (true)
        {
            //moving

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (transform.position.x > -M_GameConfig.M_MaxSidePosition)
                    transform.position += new Vector3(-1, 0, 0) * m_move_speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (transform.position.x < M_GameConfig.M_MaxSidePosition)
                    transform.position += new Vector3(1, 0, 0) * m_move_speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += new Vector3(0, 1, 0) * m_move_speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (transform.position.y > M_GameConfig.M_MaxDownPosition)
                    transform.position += new Vector3(0, -1, 0) * m_move_speed * Time.deltaTime;
            }

            //shoot
            temp_Time += Time.deltaTime;
            if (temp_Time > M_Total_Time)
            {
                temp_Time = 0;
                AttackEnemy();
            }
            yield return null;
        }
    }

    private void AttackEnemy()
    {
        switch (m_PlayerType)
        {
            case EPlayerType.SingleCharge:
                ChargeBullet(transform.position);
                break;
            case EPlayerType.DoubleBullet:
                ChargeBullet(transform.position + transform.right);
                ChargeBullet(transform.position + transform.right * -1);
                break;
            case EPlayerType.RoundRobin:
                RoundCharge(transform.position);
                break;
        }
    }

    private void RoundCharge(Vector3 position)
    {
        InstantiateBullet(0f, position);
        InstantiateBullet(-25f, position);
        InstantiateBullet(25f, position);
        if (m_Is_Triple_Shot)
        {
            InstantiateBullet(45f, position);
            InstantiateBullet(-45f, position);
        }
    }

    private void ChargeBullet(Vector3 position)
    {
        InstantiateBullet(0f, position);
        if (m_Is_Triple_Shot)
        {
            InstantiateBullet(-25f, position);
            InstantiateBullet(25f, position);
        }
    }

    private void InstantiateBullet(float rotation, Vector3 position)
    {
        PlayerBullet bullet = M_PoolManager.M_Player_Bullet_Pool.Request(position);
        bullet.transform.rotation = transform.rotation;
        bullet.transform.Rotate(Vector3.forward, rotation);
        bullet.PlayerAudio();
        bullet.M_Pool = M_PoolManager.M_Player_Bullet_Pool;
    }

    private void OnDisable()
    {
        if (tripleShotCoroutine != null)
        {
            StopCoroutine(tripleShotCoroutine);
        }
    }

    public void ChangeType()
    {
        transform.GetChild(m_playerType_int).gameObject.SetActive(false);
        m_playerType_int++;
        if(m_playerType_int == 3)
            m_playerType_int = 0;
        transform.GetChild(m_playerType_int).gameObject.SetActive(true);
        m_PlayerType = (EPlayerType)m_playerType_int;
    }
}

public enum EPlayerType
{
    SingleCharge = 0,
    DoubleBullet = 1,
    RoundRobin = 2
}