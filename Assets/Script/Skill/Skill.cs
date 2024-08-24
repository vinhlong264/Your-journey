using UnityEngine;
public class Skill : MonoBehaviour
{
    [SerializeField] protected float coolDownTimer;
    [SerializeField] protected float coolDown;
    protected Player player;

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }
    
    protected virtual void Update()
    {
        coolDownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if(coolDownTimer <= 0f)
        {
            //Sử dụng skill
            UseSkill();
            coolDownTimer = coolDown;
            return true;
        }
        Debug.Log("Đang hồi chiêu");
        return false;
    }

    public virtual void UseSkill()
    {
        // sử dụng những skill được chỉ định
    }

    // Hàm dùng để tìm ra vị trí gần nhất của Enemy vs các Skill cần lấy vị trí
    protected virtual Transform findToClosestEnemy(Transform _checkTransform) 
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(_checkTransform.position, 25);
        float closesDistance = Mathf.Infinity; // đại diện 1 giá trị dương vô cùng

        Transform closestEnemy = null;

        foreach (Collider2D hit in col)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distaceToEnenmy = Vector2.Distance(_checkTransform.position, hit.transform.position); // lấy ra khoảng cách của Player và Enemy
                //Debug.Log(distaceToEnenmy);
                if (distaceToEnenmy < closesDistance) // nếu khoảng cách lấy ra nhỏ hơn khoảng cách đã lưu
                {
                    closesDistance = distaceToEnenmy; // gán lại giá trị closesDistance
                    closestEnemy = hit.transform; // lấy ra vị trí của Enemy
                    //Debug.Log("closestEnemy: " + closestEnemy);

                }
            }
        }

        return closestEnemy;
    }
}
