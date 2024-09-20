using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/ItemEffect/ThunderStrike")]
public class thunderStrikeSO : itemEffectSO
{
    public override void excuteEffect(Transform _enemyPos)
    {
        bool isThirty = player._attackState.comboCounter == 2;

        if (isThirty)
        {
            GameObject newThunder = Instantiate(objEffect, _enemyPos.position, Quaternion.identity);
            Destroy(newThunder, 1f);
        }

    }
}
