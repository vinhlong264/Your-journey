using UnityEngine;

public class effectControllerBase : MonoBehaviour // Class base về các effect
{
    protected PlayerStatus playerStatus;
    protected virtual void Start()
    {
        playerStatus = PlayerManager.Instance.player.GetComponent<PlayerStatus>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            EnemyStatus enemyStatus = collision.GetComponent<EnemyStatus>();
            if (enemyStatus != null)
            {
                playerStatus.doDameMagical(enemyStatus);
            }
        }
    }


}
