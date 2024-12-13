
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Equipment")]
public class ItemEquipmentSO : itemDataSO
{
    public EqipmentType EqipmentType;
    [TextArea]
    [SerializeField] private string descriptionEquipment; // Thông tin skin
    public float coolDownEffect; // Thời gian tồn tại hiệu ứng
    public itemEffectSO[] ItemEffect; // Hiệu ứng


    //Các chỉ số cộng thêm khi trang bị
    [Header("Major stat")] 
    public int strength;
    public int ability;
    public int inteligent;
    public int vitality;

    [Header("Offensive stat info")]
    public int dame;
    public int critChance;
    public int critPower;

    [Header("Defend stat info")]
    public int maxHealth;
    public int armor;
    public int evasion;
    public int magicResistance;

    [Header("Magical stat")]
    public int fireDame;
    public int iceDame;
    public int lightingDame;

    public List<InventoryItem> craft; // List chứa nguyên liệu để có thể chế tạo

    private int descriptionLenght;

    public void excuteItemEffect(Transform _enemyPos) // Effect
    {
        foreach (var item in ItemEffect)
        {
            item.excuteEffect(_enemyPos);
        }
    }

    public void addModifier() // cộng thêm chỉ số
    {
        PlayerStatus playerStatus = PlayerManager.Instance.player.GetComponent<PlayerStatus>();

        //Major status
        playerStatus.strength.addModifiers(strength);
        playerStatus.ability.addModifiers(ability);
        playerStatus.inteligent.addModifiers(inteligent);
        playerStatus.vitality.addModifiers(vitality);

        //Offensive status
        playerStatus.dame.addModifiers(dame);
        playerStatus.critRate.addModifiers(critChance);
        playerStatus.critPower.addModifiers(critPower);

        //Defend status
        playerStatus.maxHealth.addModifiers(maxHealth);
        playerStatus.armor.addModifiers(armor);
        playerStatus.evasion.addModifiers(evasion);
        playerStatus.magicResistance.addModifiers(magicResistance);

        //Magical status
        playerStatus.fireDame.addModifiers(fireDame);
        playerStatus.iceDame.addModifiers(iceDame);
        playerStatus.lightingDame.addModifiers(lightingDame);
    }

    public void removeModifier() // Xóa chỉ số
    {
        PlayerStatus playerStatus = PlayerManager.Instance.player.GetComponent<PlayerStatus>();

        //Major status
        playerStatus.strength.removeModifiers(strength);
        playerStatus.ability.removeModifiers(ability);
        playerStatus.inteligent.removeModifiers(inteligent);
        playerStatus.vitality.removeModifiers(vitality);

        //Offensive status
        playerStatus.dame.removeModifiers(dame);
        playerStatus.critRate.removeModifiers(critChance);
        playerStatus.critPower.removeModifiers(critPower);

        //Defend status
        playerStatus.maxHealth.removeModifiers(maxHealth);
        playerStatus.armor.removeModifiers(armor);
        playerStatus.evasion.removeModifiers(evasion);
        playerStatus.magicResistance.removeModifiers(magicResistance);

        //Magical status
        playerStatus.fireDame.removeModifiers(fireDame);
        playerStatus.iceDame.removeModifiers(iceDame);
        playerStatus.lightingDame.removeModifiers(lightingDame);
    }

    public override string GetDescription() // Hàm để cập nhập thông tin trang bị
    {
        sb.Length = 0;
        descriptionLenght = 0;

        //Major stat
        addItemDescription(strength, StatType.Strength);
        addItemDescription(ability, StatType.Ability);
        addItemDescription(inteligent, StatType.inteligent);
        addItemDescription(vitality, StatType.vitality);

        //Offensive stat
        addItemDescription(dame , StatType.Dame);
        addItemDescription(critChance, StatType.CritRate);
        addItemDescription(critPower, StatType.CritPower);

        //Defend stat
        addItemDescription(maxHealth, StatType.Health);
        addItemDescription(armor, StatType.Armor);
        addItemDescription(evasion, StatType.Evasion);
        addItemDescription(magicResistance, StatType.MagicResitance);

        //Magic stat
        addItemDescription(fireDame, StatType.FireDame);
        addItemDescription(iceDame, StatType.IceDame);
        addItemDescription(lightingDame, StatType.LightingDame);


        if(descriptionLenght < 5)
        {
            for (int i = 0; i < 5 - descriptionLenght; i++)
            {
                sb.AppendLine();
                sb.Append("");
            }
        }

        if(descriptionEquipment.Length > 0)
        {
            sb.AppendLine();
            sb.Append(descriptionEquipment);
        }
        return sb.ToString();
    }

    private void addItemDescription(int _value , StatType type)
    {
        if(_value != 0)
        {
            if(sb.Length > 0)
            {
                sb.AppendLine();
            }

            if(_value > 0)
            {
                sb.Append("+ " + _value + " " + type);
            }

            descriptionLenght++;
        } 
    }

}
