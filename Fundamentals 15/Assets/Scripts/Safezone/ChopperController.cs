using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopperController : MonoBehaviour {
	public Transform rescuePoint;

	[SerializeField] private int chopperSpawnDistance = 50;
	[SerializeField] private int speed = 10;
	[SerializeField] private int landedTime = 3;
	[SerializeField] private float descendScale = 1f;
	[SerializeField] private float ascendScale = 2f;

	private State state = State.Entering;
	private Vector3 exitPoint;
	private Rigidbody2D rb2D;

	private enum State {
		Entering,
		Landing,
		Exiting,
	}

	private void Start() {
		rb2D = GetComponent<Rigidbody2D>();

		Vector3 randDir = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
		randDir.Normalize();
		transform.Translate(randDir * chopperSpawnDistance);
	}

	// Update is called once per frame
	void Update() {
		switch (state) {
			case State.Entering:
				MoveTo(rescuePoint.position);
				break;
			case State.Exiting:
				MoveTo(exitPoint);
				break;
		}
	}

	private void MoveTo(Vector3 destination) {
		Vector2 direction = destination - transform.position;

		if (direction.sqrMagnitude > 1)
			direction.Normalize();

		rb2D.velocity = direction * speed;

		Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
		transform.rotation = rotation;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (!other.CompareTag("Safezone"))
			return;

		state = State.Landing;
		StartCoroutine(LoadHumans());
	}

	private IEnumerator LoadHumans() {
		rb2D.velocity = Vector2.zero;

		float t = 0f;
		while (t < ascendScale - descendScale) {
			transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, 0);
			t += Time.deltaTime;
			yield return null;
		}

		t = 0f;
		while (t < landedTime) {
			t += Time.deltaTime;
			yield return null;
		}

		rescuePoint.gameObject.GetComponent<HumanRescue>().RescueHumans();

		Vector3 newDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
		newDir.Normalize();
		// Get the chopper out of the map
		exitPoint = newDir * 100;

		t = 0f;
		while (t < ascendScale - descendScale) {
			transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, 0);
			t += Time.deltaTime;
			yield return null;
		}

		GetComponent<CircleCollider2D>().enabled = false;
		state = State.Exiting;
	}

	private void OnBecameInvisible() {
		if (state == State.Exiting)
			Destroy(gameObject);
	}
}
