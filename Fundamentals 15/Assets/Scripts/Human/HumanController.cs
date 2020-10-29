using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour {
	public Transform target;
	public bool isRescued = false;

	[SerializeField] private int speed = 2;
	private Rigidbody2D rb2D;
	private Camera playerCamera;
	private bool isCloseToHuman;
	private bool isOrdered = false;


	// Start is called before the first frame update
	private void Start() {
		target = GameObject.FindWithTag("Player").transform;
		rb2D = GetComponent<Rigidbody2D>();
		playerCamera = Camera.main;
	}

	private void Update() {
		if (!Input.GetButtonDown("Fire2") || isRescued) return;
		if (isOrdered)
			Destroy(target.gameObject);

		Vector2 mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
		GameObject waypoint = new GameObject("Waypoint") {tag = "Waypoint"};

		waypoint.transform.position = mousePosition;
		target = waypoint.transform;

		isOrdered = true;
	}

	private void FixedUpdate() {
		Vector2 direction = target.position - transform.position;
		if (direction.sqrMagnitude > 1)
			direction.Normalize();

		if (isOrdered || !isCloseToHuman || isRescued)
			rb2D.velocity = direction * speed;
		else
			rb2D.velocity = Vector2.zero;

		RotateTowards(direction);
	}

	private void RotateTowards(Vector3 direction) {
		Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
		transform.rotation = rotation;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player"))
			isCloseToHuman = true;
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Player"))
			isCloseToHuman = false;
	}
}
