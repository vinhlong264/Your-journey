using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/ItemEffect/FreezeEffect")]
public class freezeEffectSO : itemEffectSO
{
    [SerializeField] private float durationTime;
    private Inventory inventory = GameManager.Instance.Inventory;

    public override void excuteEffect(Transform _transfomTarget)
    {
        PlayerStats playerStatus = player.GetComponent<PlayerStats>();

        if(playerStatus?.currentHealth > playerStatus?.getMaxHealth() * 0.1f) return; // Tạo ra kĩ năng khi hp dưới 10% sẽ gây đóng băng xung quanh

        if(!inventory.canUseArmor()) return; //Nếu vẫn đang trong thời gian hồi thì k thực thi

        Collider2D[] col = Physics2D.OverlapCircleAll(_transfomTarget.position, 2f);
        foreach(var hit in col)
        {
            hit.GetComponent<Enemy>()?.FreezeBy(durationTime);
        }
    }
}
