using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "Data/ItemEffect/healEffect")]
public class healEffectSO : itemEffectSO
{
    [Range(0f, 1f)]
    [SerializeField] private float healPerecent;
    public override void excuteEffect(Transform _enemyPos)
    {
        PlayerStats playerStatus = GameManager.Instance.player.GetComponent<PlayerStats>();
        if (playerStatus == null) return;

        int healthAmount = Mathf.RoundToInt(playerStatus.getMaxHealth() * healPerecent);

        playerStatus.restoreHealthBy(healthAmount);
    }
}
