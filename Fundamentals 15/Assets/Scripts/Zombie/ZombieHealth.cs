using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ZombieHealth : MonoBehaviour
{
	public int startHealth = 3;
	private int currentHealth;
	[SerializeField] GameObject deathVFX; // ToDo - Create/add deathVFX
	[SerializeField] private GameObject human;
	public AudioClip hurtSound;
	public AudioClip deathSound;
	AudioSource audioSource;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		currentHealth = startHealth;
	}
	public void DealDamage(int damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0) {
			TriggerDeathVFX();
			return;
		}
		audioSource.clip = hurtSound;
		audioSource.Play();
	}

	private void TriggerDeathVFX()
	{
		if (!deathVFX)
			return;

		GameObject deathVFXObject = Instantiate(deathVFX, transform.position, deathVFX.transform.rotation);
		
		var deathAS = deathVFXObject.GetComponent<AudioSource>();
		deathAS.clip = deathSound;
		deathAS.Play();

		Destroy(gameObject);
	}
	
	public void HumanTransformation() {
		Instantiate(human, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
