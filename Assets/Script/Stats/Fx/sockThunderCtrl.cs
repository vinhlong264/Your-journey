using UnityEngine;

public class sockThunderCtrl : MonoBehaviour
{
    [SerializeField] private EnemyStats target; 
    private float speed = 35;
    private Animator anim;
    private bool isTrigger;

    private int dame;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void setUpThunder(int _dame , EnemyStats _target)
    {
        dame = _dame;
        target = _target;
    }
    void Update()
    {
        if(!target) return;

        if (isTrigger) return;

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position , target.transform.position) < 0.1f)
        {
            transform.localRotation = Quaternion.identity;
            anim.transform.localRotation = Quaternion.identity;

            transform.localRotation = Quaternion.identity;
            anim.transform.localScale = new Vector3(3, 3);

            Invoke("DameBy", 0.1f);
            isTrigger = true;
            anim.SetTrigger("isHit");
        }
    }


    void DameBy()
    {
        target.ApplyShock(true);
        target.takeDame(1);
        Destroy(gameObject, 0.4f);
    }
}
