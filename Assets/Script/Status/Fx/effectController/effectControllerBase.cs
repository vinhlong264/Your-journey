using UnityEngine;

public class effectControllerBase : MonoBehaviour // Class base về các effect
{
    protected PlayerStats playerStatus;
    protected virtual void Start()
    {
        playerStatus = PlayerManager.Instance.player.GetComponent<PlayerStats>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            IDameHandleMagical dameMagical = collision.GetComponent<IDameHandleMagical>();
            if (dameMagical != null)
            {
                dameMagical.DameDoMagical(playerStatus);
            }
        }
    }


}
