using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/ItemEffect/BuffEfect")]

public class buffEffectSO : itemEffectSO
{
    private PlayerStatus status;
    [SerializeField] private StatType buffType;
    [SerializeField] private float buffDurartion;
    [SerializeField] private int amountBuff;

    public override void excuteEffect(Transform _enemyPos)
    {
        status = PlayerManager.Instance.player.GetComponent<PlayerStatus>();
        status.increaseModfierStatus(amountBuff, buffDurartion, status.getStat(buffType));
    }
}
