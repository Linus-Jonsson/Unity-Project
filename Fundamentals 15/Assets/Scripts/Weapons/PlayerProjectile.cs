using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {
	//syringe
	public GameObject hitEffekt;
	Rigidbody2D rb;
	public AudioClip hitNotZombieSound;
	AudioSource audioSource;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
	}

	private void Update() {
		if (!transform.GetChild(0).GetComponent<SpriteRenderer>().isVisible) {
			Destroy(gameObject);
		}
	}

	bool hasHit = false;

	private void OnCollisionEnter2D(Collision2D collision) {
		var tag = collision.gameObject.tag;
		if (hasHit == false) {//hade bug med att den träffa flera samtidigt
			hasHit = true;
			Destroy(GetComponent<Collider2D>());
			rb.velocity = Vector2.zero;
			Destroy(rb);
			Destroy(GetComponent<TrailRenderer>());
			transform.parent = collision.transform;

			if (tag == "Zombie") {
				ContactPoint2D contact = collision.contacts[0];
				Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
				Instantiate(hitEffekt, contact.point, rot);
				collision.gameObject.GetComponent<ZombieHealth>().HumanTransformation();
			}
			else {
				audioSource.clip = hitNotZombieSound;
				audioSource.Play();
				Destroy(gameObject, 10f); //destory ifall den ej krocka med zombie
			}
		}
	}
}
