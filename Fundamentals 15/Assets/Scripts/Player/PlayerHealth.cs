using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	public int startHealth = 3;

	[SerializeField] private float invulnerabilityDuration = 1;
	[SerializeField] private float blinkSpeed = 0.2f;
	[SerializeField] private Color hurtColor;

	private int currentHealth;
	private Rigidbody2D rb2D;
	private PlayerHealthUI playerHealthUI;
	private bool invulnerable = false;
	private SpriteRenderer spriteRenderer;
	private Color startColor;

	void Start() {
		currentHealth = startHealth;
		rb2D = GetComponent<Rigidbody2D>();

		playerHealthUI = GameObject.FindGameObjectWithTag("HealthUI").GetComponent<PlayerHealthUI>();
		spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
		startColor = spriteRenderer.color;
	}

	public void Damage(int damage, GameObject attacker, int force) {
		if (invulnerable)
			return;

		currentHealth -= damage;
		playerHealthUI.RemoveFromUi(damage);
		rb2D.AddForce(attacker.transform.up * force, ForceMode2D.Force);

		StartCoroutine(Invulnerability(invulnerabilityDuration));

		if (currentHealth <= 0)
			FindObjectOfType<LevelController>().HandleLoseCondition();
	}

	private IEnumerator Invulnerability(float duration) {
		invulnerable = true;
		Coroutine blinkCoroutine = StartCoroutine(SpriteBlink(duration));

		float t = 0;
		while (t < duration) {
			t += Time.deltaTime;
			yield return null;
		}

		StopCoroutine(blinkCoroutine);
		spriteRenderer.color = startColor;
		invulnerable = false;
	}

	private IEnumerator SpriteBlink(float duration) {
		bool blink = true;
		for (float i = 0; i < duration; i += blinkSpeed) {
			spriteRenderer.color = blink ? hurtColor : startColor;
			blink = !blink;

			yield return new WaitForSeconds(blinkSpeed);
		}
	}
}
