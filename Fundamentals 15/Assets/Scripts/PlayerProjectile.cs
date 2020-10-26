using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public GameObject hitEffekt;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag;

        rb.velocity = Vector2.zero;
        Destroy(rb);
        Destroy(GetComponent<Collider2D>());
        Destroy(GetComponent<TrailRenderer>());
        transform.parent = collision.transform;

        //if tag = zombie?
        ContactPoint2D contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Instantiate(hitEffekt, contact.point, rot);

        //om collison tag = Zombie
        //Zombie.cure
    }
}
