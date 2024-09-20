using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall_Controller : MonoBehaviour
{
    [SerializeField] private Vector3 maxSize;
    [SerializeField] private float speedGrown;
    [SerializeField] private bool isGrown;
    [SerializeField] private Transform target;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrown)
        {
            transform.localScale = Vector3.Lerp(transform.localScale , maxSize, speedGrown * Time.deltaTime);
            if(transform.localScale.x > 4f)
            {
                transform.position = Vector2.MoveTowards(transform.position , target.position , 5f * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("fireTrigger", 1);
    }

    private void fireTrigger()
    {
        anim.SetTrigger("isTrigger");
        Destroy(gameObject,.5f);
    }
}
