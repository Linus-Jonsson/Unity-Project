using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private int speed = 1;
    private Transform playerTransform;
	private Rigidbody2D rb2D;
	private Vector2 direction;

	// Start is called before the first frame update
	void Start()
	{
		playerTransform = GameObject.FindWithTag("Player").transform;
		rb2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		direction = playerTransform.position - transform.position;
		direction.Normalize();

		Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
		transform.rotation = rotation;
	}

	void FixedUpdate()
	{
		rb2D.velocity = direction * speed;
	}
}
