using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private Animator anim;
    private Collider2D cd;
    [SerializeField] private bool canRotation = true;

    private Player player;
    [SerializeField] private bool isReturning;
    private float speedReturning = 12;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        anim = GetComponentInChildren<Animator>();
    }

    public void setupSword(Vector2 _dir, float _gravityScale , Player _player) // hàm quản lý vật lý chuyển động của sword
    {
        player = _player;
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;

        anim.SetBool("Rotation", true);
    }

    private void FixedUpdate()
    {
        if(canRotation)
         transform.right = rb.velocity;


        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speedReturning * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < 2)
            {
                player.ClearSword();
            }
        }
    }

    public void ReturSword()
    {
        rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetBool("Rotation", false);

        canRotation = false;
        cd.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        
        transform.parent = collision.transform;
    }
}
