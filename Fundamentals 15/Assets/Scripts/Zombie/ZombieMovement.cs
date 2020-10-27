using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
	[SerializeField] private int huntPlayerChance = 50;
	[SerializeField] private int speed = 1;
  private Transform target;
	private Rigidbody2D rb2D;
	private Vector2 direction;

	// Start is called before the first frame update
	void Start()
	{
		rb2D = GetComponent<Rigidbody2D>();

		if (Random.Range(0, 100) < huntPlayerChance)
			HuntPlayer();
		else
			HuntRandomHuman();
	}

	// Update is called once per frame
	void Update()
	{
		direction = target.position - transform.position;
		direction.Normalize();

		Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
		transform.rotation = rotation;
	}

	private void HuntPlayer() {
		target = GameObject.FindWithTag("Player").transform;
	}

	private void HuntRandomHuman() {
		GameObject[] humans = GameObject.FindGameObjectsWithTag("Human");
		if (humans.Length <= 0) {
			HuntPlayer();
		}

		GameObject human = humans[Random.Range(0, humans.Length - 1)];
		target = human.transform;
	}

	void FixedUpdate()
	{
		rb2D.velocity = direction * speed;
	}
}
