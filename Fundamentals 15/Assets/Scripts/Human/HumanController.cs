using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour {
	public Transform target;

	[SerializeField] private int speed = 2;
	private Rigidbody2D rb2D;

	// Start is called before the first frame update
	private void Start() {
		target = GameObject.FindWithTag("Player").transform;
		rb2D = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate() {
		Vector3 direction = target.position - transform.position;
		if (direction.sqrMagnitude > 1)
			direction.Normalize();

		rb2D.velocity = direction * speed;

		RotateTo(direction);
	}

	private void RotateTo(Vector3 direction) {
		Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
		transform.rotation = rotation * Quaternion.Euler(0, 0, 90);
	}
}
