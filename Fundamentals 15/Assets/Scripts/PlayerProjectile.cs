using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;
        Destroy(rb);
        Destroy(GetComponent<Collider2D>());
        Destroy(GetComponent<TrailRenderer>());
        transform.parent = collision.transform;

        //om collison tag = Zombie
        //Zombie.cure
    }
}
