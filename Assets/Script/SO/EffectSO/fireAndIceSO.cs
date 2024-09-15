using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/ItemEffect/fireAndIce")]
public class fireAndIceSO : itemEffectSO
{
    [SerializeField] private float xVelocity;
    public override void excuteEffect(Transform _respawnPos)
    {
        Player player = PlayerManager.Instance.player;

        bool thirtyAttack = player._attackState.comboCounter == 2;

        if (!thirtyAttack) return;

        GameObject newFireIce = Instantiate(objEffect, _respawnPos.position, player.transform.rotation);
        if (newFireIce != null)
        {
            newFireIce.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * player.isFacingDir , 0);
            Destroy(newFireIce,10f);
        }
    }
}
