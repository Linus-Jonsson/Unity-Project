using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    public GameObject hitEffekt;
    Rigidbody2D rb;
    public AudioClip hitSound;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    [HideInInspector]

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator ExplodeBullet(float t)
    {
        yield return new WaitForSeconds(t);
        {
            DestroyBullet(transform.position);
        }
    }
    private void DestroyBullet(Vector3 pos)
    {
        GameObject bulletBoom = Instantiate(hitEffekt, pos, hitEffekt.transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag;
        if (tag == "Zombie")
        {
            collision.gameObject.GetComponent<ZombieHealth>().DealDamage(1);
        }

        audioSource.clip = hitSound;
        audioSource.Play();

        ContactPoint2D contact = collision.GetContact(0);
        DestroyBullet(contact.point);
    }
}
