using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offence : MonoBehaviour
{
    private float timeBtwAttack;

    public float startTimeBtwAttack;
    public float attackRange;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public int damage;

    void Start()
    {

    }

    void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<damage>().TakeDamage(damage);
                }
            }


            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
