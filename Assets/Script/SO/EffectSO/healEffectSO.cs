using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "Data/ItemEffect/healEffect")]
public class healEffectSO : itemEffectSO
{
    [Range(0f, 1f)]
    [SerializeField] private float healPerecent;
    public override void excuteEffect(Transform _enemyPos)
    {
        PlayerStatus playerStatus = PlayerManager.Instance.player.GetComponent<PlayerStatus>();
        if (playerStatus == null) return;

        int healthAmount = Mathf.RoundToInt(playerStatus.getMaxHealth() + healPerecent);

        playerStatus.increaseHealthBy(healthAmount);
    }
}
