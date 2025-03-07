using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDemoSlime : MonoBehaviour
{
    private EnemyStats enemyStats;
    [SerializeField] private LayerMask mask;

    public void setUpSpell(EnemyStats _enemyStats)
    {
        this.enemyStats = _enemyStats;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || 
            collision.gameObject.CompareTag("Player")
            || collision.gameObject.CompareTag("Enemy"))
        {

            Debug.Log("Collision Ground");
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.DameDoMagical(enemyStats);
            }
            gameObject.SetActive(false);
        }
    }
}
