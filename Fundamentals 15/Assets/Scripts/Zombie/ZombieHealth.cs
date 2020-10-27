using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ZombieHealth : MonoBehaviour
{
    [SerializeField] int health = 1;
    [SerializeField] GameObject deathVFX; // ToDo - Create/add deathVFX
    [SerializeField] Sprite[] spriteArray;
    public AudioClip hurtSound;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void DealDamage(int damage)
    {
        health -= damage;
        audioSource.clip = hurtSound;
        audioSource.Play();

        if (health <= 0) {
            TriggerDeathVFX();
            HumanTransformation();
        }
    }

    private void TriggerDeathVFX()
    {
        if (!deathVFX)
            return;
        GameObject deathVFXObject = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(deathVFXObject, 1f);
    }
    
    void HumanTransformation()
    {
            GetComponentInChildren<SpriteRenderer>().sprite = spriteArray[1];
            GetComponent<ZombieMovement>().enabled = false;
            GetComponent<HumanController>().enabled = true;
            tag = "Human";
            name = "Human";
    }
}
