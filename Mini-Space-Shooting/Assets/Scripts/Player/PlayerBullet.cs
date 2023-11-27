using Pool;
using System;
using UnityEngine;

/// <summary>
/// m_Player Bullet
/// </summary>
public class PlayerBullet : MonoBehaviour
{
	[Header("Parameter")]
	public float m_move_speed = 5;
	public float m_life_time = 2;
	public EAudioTag m_Audio_Tag;
	private float m_Total_Time;

	private StageLoop m_stage_Loop => StageLoop.Instance;
    public IPool<PlayerBullet> M_Pool
    {
		private get;
		set;      
    }

    private void Start()
    {
        m_Total_Time = m_life_time;
        m_stage_Loop.GameOverEvent += OnGameOver;
    }

    private void OnGameOver()
    {
		M_Pool.Return(this);
    }

    public void PlayerAudio()
	{
		StageLoop.Instance.PlayAudio(m_Audio_Tag);
	}
    //
    void Update()
	{
		transform.position += transform.up * m_move_speed * Time.deltaTime;

		m_life_time -= Time.deltaTime;
		if (m_life_time <= 0)
		{
			ReturnToThePool();
		}
	}

	public void ReturnToThePool()
	{
		m_life_time = m_Total_Time;
		M_Pool.Return(this);
	}

    private void OnDestroy()
    {
        m_stage_Loop.GameOverEvent -= OnGameOver;
    }
}
