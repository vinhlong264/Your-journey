using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/ItemEffect/BuffEfect")]

public class buffEffectSO : itemEffectSO
{
    private PlayerStats status;
    [SerializeField] private StatType buffType;
    [SerializeField] private float buffDurartion;
    [SerializeField] private int amountBuff;

    public override void excuteEffect(Transform _enemyPos)
    {
        status = GameManager.Instance.player.GetComponent<PlayerStats>();
        status.increaseModfierStatus(amountBuff, buffDurartion, status.getStat(buffType));
    }
}
