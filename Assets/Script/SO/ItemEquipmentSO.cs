
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Equipment")]
public class ItemEquipmentSO : itemDataSO
{
    public EqipmentType EqipmentType;
    public itemEffectSO[] ItemEffect;

    [Header("Major status")]
    public int strength;
    public int ability;
    public int inteligent;
    public int vitality;

    [Header("Offensive status info")]
    public int dame;
    public int critChance;
    public int critPower;

    [Header("Defend status info")]
    public int maxHealth;
    public int armor;
    public int evasion;
    public int magicResistance;

    [Header("Magical status")]
    public int fireDame;
    public int iceDame;
    public int lightingDame;

    public List<InventoryItem> craft;

    public void excuteItemEffect()
    {
        foreach(var item in ItemEffect)
        {
            item.excuteEffect();
        }
    }

    public void addModifier()
    {
        PlayerStatus playerStatus = PlayerManager.Instance.player.GetComponent<PlayerStatus>();
        
        //Major status
        playerStatus.strength.addModifiers(strength);
        playerStatus.ability.addModifiers(ability);
        playerStatus.inteligent.addModifiers(inteligent);
        playerStatus.vitality.addModifiers(vitality);

        //Offensive status
        playerStatus.dame.addModifiers(dame);
        playerStatus.critChance.addModifiers(critChance);
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

    public void removeModifier()
    {
        PlayerStatus playerStatus = PlayerManager.Instance.player.GetComponent<PlayerStatus>();

        //Major status
        playerStatus.strength.removeModifiers(strength);
        playerStatus.ability.removeModifiers(ability);
        playerStatus.inteligent.removeModifiers(inteligent);
        playerStatus.vitality.removeModifiers(vitality);

        //Offensive status
        playerStatus.dame.removeModifiers(dame);
        playerStatus.critChance.removeModifiers(critChance);
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

}
public enum EqipmentType
{
    Sword,
    Armor,
    Helmet,
    Bottle
}
