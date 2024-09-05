using UnityEngine;

public class ThunderController : MonoBehaviour
{
    [SerializeField] CharacterStatus target;
    [SerializeField] private float speed;
    private Animator anim;
    private bool isTrigger;

    private int dame;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void setUpThunder(int _dame , CharacterStatus _target)
    {
        dame = _dame;
        target = _target;
    }
    void Update()
    {
        if (isTrigger) return;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position , target.transform.position) < 0.1f)
        {
            anim.transform.localRotation = Quaternion.identity;
            transform.localRotation = Quaternion.identity;
            anim.transform.localScale = new Vector3(2, 2);

            Invoke("DameBy", 0.1f);
            isTrigger = true;
            anim.SetTrigger("isHit");
        }
    }


    void DameBy()
    {
        target.AplyShock(true);
        target.takeDame(1);
        Destroy(gameObject, 0.4f);
    }
}
