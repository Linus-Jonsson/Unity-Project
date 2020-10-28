using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour {
	[SerializeField] private int damage = 1;
	[SerializeField] private int knockbackForce = 2;
	

	private ZombieMovement controller;

	private void Start() {
		controller = GetComponent<ZombieMovement>();
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (!other.gameObject.CompareTag("Human") && !other.gameObject.CompareTag("Player"))
			return;

		if (controller.gameObject != other.gameObject)
			controller.target = other.gameObject.transform;

		if (other.gameObject.CompareTag("Human")) {
			other.gameObject.GetComponent<HumanHealth>().Damage(damage);
		} else if (other.gameObject.CompareTag("Player"))
			other.gameObject.GetComponent<PlayerHealth>().Damage(damage, gameObject, knockbackForce);
	}
}
