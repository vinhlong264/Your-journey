using UnityEngine;

public class WaterBullet : MonoBehaviour
{
    private float speed;
    private bool canMove;
    private Transform target;
    private EnemyStats enemyStats;

    private Animator animator;

    public void setUp(float _speed , bool _canMove , Transform _target , EnemyStats _stats)
    {
        this.speed = _speed;
        this.canMove = _canMove;
        this.target = _target;
        enemyStats = _stats;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canMove)
        {
            animator.SetTrigger("canMove");
            transform.position = Vector2.MoveTowards(transform.position , target.position , speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IDameHandleMagical magicalHandel = collision.GetComponent<IDameHandleMagical>();
            if (magicalHandel != null)
            {
                magicalHandel.DameDoMagical(enemyStats);
                Destroy(gameObject, 0.5f);
            }
        }
    }
}
