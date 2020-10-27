﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour {
	[SerializeField] private int speed = 5;
	private Rigidbody2D rb2D;
	private Vector2 input;

	// Start is called before the first frame update
	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update() {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		input = new Vector2(x, y);

		if (input.sqrMagnitude > 1)
			input.Normalize();

		LookAtMouse();
	}

	void FixedUpdate() {
		rb2D.velocity = input * speed;
	}

	void LookAtMouse()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
	}
}
