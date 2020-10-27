using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ZombieHealth : MonoBehaviour
{
    public int startHealth = 3;
    private int currentHealth = 1;
    [SerializeField] GameObject deathVFX; // ToDo - Create/add deathVFX
    [SerializeField] Sprite[] spriteArray;
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
        Destroy(deathVFXObject, deathAS.clip.length);
        Destroy(gameObject);

    }
    
    public void HumanTransformation()
    {
            GetComponentInChildren<SpriteRenderer>().sprite = spriteArray[1];
            GetComponent<ZombieMovement>().enabled = false;
            GetComponent<HumanController>().enabled = true;
            gameObject.tag = "Human";
            gameObject.name = "Human";
    }
}
