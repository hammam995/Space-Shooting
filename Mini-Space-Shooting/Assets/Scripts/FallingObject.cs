using Pool;
using System;
using System.Collections;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private StageLoop m_stageLoop => StageLoop.Instance;
    private Rigidbody m_RigidBody;
    private int m_Rotation => m_stageLoop.M_Game_Config.M_FallingObjectRotation;
    public IPool<FallingObject> M_Pool
    {
        private get; set;
    }
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_stageLoop.GameOverEvent += OnGameOver;
    }

    private void OnGameOver()
    {
        M_Pool.Return(this);
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(MainCoroutine());
    }

    private IEnumerator MainCoroutine()
    {
        while (!(m_stageLoop ==null || m_stageLoop.M_Game_Over))
        {
            //move
            transform.position += new Vector3(0, -1, 0) * m_stageLoop.M_Move_Speed * Time.deltaTime;

            //animation
            transform.rotation *= Quaternion.AngleAxis(m_Rotation * Time.deltaTime, new Vector3(1, 1, 0));

            if (transform.position.y < -8)
            {
                m_stageLoop.M_Passed_Enemies++;
                ReturnToPool();
                yield break;
            }
            yield return null;
        }
    }

    public void ReturnToPool()
    {
        m_RigidBody.velocity = Vector3.zero;
        m_RigidBody.angularVelocity = Vector3.zero;
        M_Pool.Return(this);
    }

    private void OnDestroy()
    {
        m_stageLoop.GameOverEvent -= OnGameOver;
    }
}
