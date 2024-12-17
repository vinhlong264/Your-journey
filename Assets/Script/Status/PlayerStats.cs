using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;
    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    protected override void Update()
    {
        base.Update();
    }


    public void DoDameWithSkill(CharacterStats _targetReceive, float percentDame)
    {
        if (AvoidAttack(_targetReceive)) return;

        int _dame = dame.getValue() + strength.getValue();
        int finalDame = Mathf.RoundToInt(_dame * (percentDame / 100));

        if (CanCrit())
        {
            finalDame = calculateCritalDame(finalDame);
        }

        finalDame = CheckTargetArmor(finalDame, _targetReceive);
        Debug.Log("DameTotal: " + finalDame);
        _targetReceive.takeDame(finalDame);
    }

    public override void DoDamePhysical(CharacterStats _targetReceive)
    {
        base.DoDamePhysical(_targetReceive);
    }

    public override void doDameMagical(CharacterStats _targetStatus)
    {
        base.doDameMagical(_targetStatus);
    }

    public override void takeDame(int _dame)
    {
        base.takeDame(_dame);
        player.dameEffect();
    }
    protected override void Die()
    {
        base.Die();
        player.Die();
    }

    protected override void decreaseHealthBy(int _dame)
    {
        base.decreaseHealthBy(_dame);

        ItemEquipmentSO currentArmor = Inventory.Instance.getEquipmentBy(EqipmentType.Armor);

        if (currentArmor != null)
        {
            currentArmor.excuteItemEffect(player.transform);
        }
    }

    public override void onEvasion()
    {
        SkillManager.instance.dogde_Skill.createMirageOnDogde(transform);
    }

    public Stat getStat(StatType type) // hàm để lấy ra Status theo type
    {
        switch (type)
        {
            case StatType.Strength:
                return strength;
            case StatType.Ability:
                return ability;
            case StatType.inteligent:
                return inteligent;
            case StatType.vitality:
                return vitality;
            case StatType.Health:
                return maxHealth;
            case StatType.Armor:
                return armor;
            case StatType.Evasion:
                return evasion;
            case StatType.MagicResitance:
                return magicResistance;
            case StatType.Dame:
                return dame;
            case StatType.CritPower:
                return critPower;
            case StatType.CritRate:
                return critRate;
            case StatType.FireDame:
                return fireDame;
            case StatType.IceDame:
                return iceDame;
            case StatType.LightingDame:
                return lightingDame;
        }
        return null;
    }
}

