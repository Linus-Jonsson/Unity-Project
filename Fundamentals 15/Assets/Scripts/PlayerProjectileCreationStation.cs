using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerProjectileCreationStation : MonoBehaviour
{
    public AudioClip craftSound;
    AudioSource audioSource;
    public GameObject craftText;
    private Transform textSpawnPos;
    PlayerShoot playerShoot;
    private bool canCraft = false;
    private bool isCrafting = false;
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetComponent<PlayerShoot>() != null)
            playerShoot = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetComponent<PlayerShoot>();
        textSpawnPos = GameObject.FindGameObjectWithTag("Player").transform;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (canCraft && Input.GetKeyDown(KeyCode.Space))
        {
            if (playerShoot.CanCraft())
            {
                if(!isCrafting)
                    StartCoroutine(Craft());        
            }
            else
            {
                SpawnText("Full inventory"); //GItHub Test
            }
        }
    }

    IEnumerator Craft()
    {
        isCrafting = true;

        SpawnText("Crafting");
        audioSource.clip = craftSound;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        playerShoot.AddProjetiles(1);
        SpawnText("Projectile +1");

        isCrafting = false;
    }

    void SpawnText(string text)
    {
        GameObject craftedClone = Instantiate(craftText, textSpawnPos.position + (Vector3.up), craftText.transform.rotation);
        craftedClone.GetComponent<FadeTextEffekt>().UpdateText(text);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            canCraft = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            canCraft = false;
    }

}
