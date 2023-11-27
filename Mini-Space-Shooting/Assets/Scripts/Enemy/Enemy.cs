using UnityEngine;

public class Enemy : FallingObject
{
    
    public int m_score = 100;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out PlayerBullet player_bullet))
        {
            DestroyByPlayer(player_bullet);
        }
    }

    void DestroyByPlayer(PlayerBullet a_player_bullet)
    {
        //add score
        if (StageLoop.Instance)
        {
            StageLoop.Instance.InstantiateHitVFXAndAddScore(m_score, this.transform.position);
        }
        StageLoop.Instance.PlayAudio(EAudioTag.DestroyFallingItem);
        //delete bullet
        if (a_player_bullet)
        {
            a_player_bullet.ReturnToThePool();
        }

        //delete self
        ReturnToPool();
    }
}
