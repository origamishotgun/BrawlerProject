using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockBackOnCollision : MonoBehaviour
{
    private float knockBackStr;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

        if (rb!=null)
        {
            Vector2 direction = collision.transform.position - transform.position;
            direction.y = 0;
            rb.AddForce(direction.normalized * knockBackStr, ForceMode2D.Impulse);
        }
    }
}
