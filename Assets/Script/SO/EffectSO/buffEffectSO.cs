using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/ItemEffect/BuffEfect")]

public class buffEffectSO : itemEffectSO
{
    private PlayerStatus status;
    [SerializeField] private BuffType buffType;
    [SerializeField] private float buffDurartion;
    [SerializeField] private int amountBuff;

    public override void excuteEffect(Transform _enemyPos)
    {
        status = PlayerManager.Instance.player.GetComponent<PlayerStatus>();
        status.increaseModfierStatus(amountBuff, buffDurartion, addModifier());
    }

    private Status addModifier()
    {
        switch (buffType)
        {
            case BuffType.Strength:
                return status.strength;
            case BuffType.Ability:
                return status.ability;
            case BuffType.inteligent:
                return status.inteligent;
            case BuffType.vitality:
                return status.vitality;
            case BuffType.Armor:
                Debug.Log("Lấy buff");
                return status.armor;
            case BuffType.Evasion:
                return status.evasion;
            case BuffType.MagicResitance:
                return status.magicResistance;
            case BuffType.Dame:
                return status.dame;
            case BuffType.CritPower:
                return status.critPower;
            case BuffType.CritChance:
                return status.critChance;
            case BuffType.FireDame:
                return status.fireDame;
            case BuffType.IceDame:
                return status.iceDame;
            case BuffType.LightingDame:
                return status.lightingDame;
        }
        return null;
    }



}


public enum BuffType
{
    //Major type status
    Strength,
    Ability,
    inteligent,
    vitality,
    //Defend type status
    Armor,
    Evasion,
    MagicResitance,

    //Dame physical status
    Dame,
    CritChance,
    CritPower,

    //Dame magical status
    FireDame,
    IceDame,
    LightingDame
}
