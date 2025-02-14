using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float dirX;
    private CharacterStats stats;
    private float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void setUp(float _dirX , CharacterStats _stats , float _speed)
    {
        this.dirX = _dirX;
        this.stats = _stats;
        this.speed = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DelayMovement());
    }

    IEnumerator DelayMovement()
    {
        yield return new WaitForSeconds(0.5f);
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerStats>() != null)
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if(playerStats != null)
            {
                playerStats.DameDoMagical(stats);
                gameObject.SetActive(false);
            }
        }
    }
}
