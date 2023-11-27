using UnityEngine;

public class PowerUp : FallingObject
{
    [SerializeField] private EPowerUp powerUp;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out PlayerBullet a_player_bullet))
        {
            DestroyByPlayer(a_player_bullet);
        }
    }

    void DestroyByPlayer(PlayerBullet a_player_bullet)
    {
        if (StageLoop.Instance)
        {
            StageLoop.Instance.M_Power_Up_Controller.OnPowerUp(powerUp);
        }
        //return bullet back to the pool
        if (a_player_bullet)
        {
            a_player_bullet.ReturnToThePool();
        }
        //return to the pool
        ReturnToPool();
    }
}
