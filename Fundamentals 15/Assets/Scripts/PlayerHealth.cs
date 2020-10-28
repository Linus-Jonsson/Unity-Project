using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	[SerializeField] private int startHealth = 3;

	private int currentHealth;
	private Rigidbody2D rb2D;

	// Start is called before the first frame update
	void Start() {
		currentHealth = startHealth;
		rb2D = GetComponent<Rigidbody2D>();
	}

	public void Damage(int damage, GameObject attacker, int force) {
		currentHealth -= damage;

		rb2D.AddForce(attacker.transform.up * force, ForceMode2D.Force);

		if (currentHealth <= 0) {
			Debug.Log("Game Over");
			// TODO: Implement game over state
		}
	}
}
