using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanHealth : MonoBehaviour {
	[SerializeField] private GameObject zombie;
	[SerializeField] private int startHealth = 1;

	private int currentHealth;

	// Start is called before the first frame update
	void Start() {
		currentHealth = startHealth;
	}

	public void Damage(int damage) {
		currentHealth -= damage;

		if (currentHealth <= 0)
			ZombieTransformation();
	}

	private void ZombieTransformation() {
		Instantiate(zombie, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}

