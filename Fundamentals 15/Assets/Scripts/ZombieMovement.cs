using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private int speed = 1;
    [SerializeField] GameObject player;
	private Rigidbody2D rb2D;
	private Vector2 direction;

	// Start is called before the first frame update
	void Start()
	{
		rb2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		direction = new Vector2(player.transform.position.x - transform.position.x,
								player.transform.position.y - transform.position.y);
		direction.Normalize();

		Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
		transform.rotation = rotation;
	}

	void FixedUpdate()
	{
		rb2D.velocity = direction * speed;
	}
}
