using UnityEngine;

public class PlayerStatus : CharacterStatus
{
    private Player player;
    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
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
}

