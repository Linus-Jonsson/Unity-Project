using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	public int startHealth = 3;
	private int currentHealth;
	private Rigidbody2D rb2D;
	private PlayerHealthUI playerHealthUI;

	void Start() {
		currentHealth = startHealth;
		rb2D = GetComponent<Rigidbody2D>();
		playerHealthUI = GameObject.FindGameObjectWithTag("HealthUI").GetComponent<PlayerHealthUI>();
	}

	public void Damage(int damage, GameObject attacker, int force) {
		currentHealth -= damage;
		playerHealthUI.RemoveFromUi(damage);
		rb2D.AddForce(attacker.transform.up * force, ForceMode2D.Force);

		if (currentHealth <= 0) {
			Debug.Log("GameOver");
			FindObjectOfType<LevelController>().HandleLoseCondition();
		}
	}
}
