using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerProjectileCreationStation : MonoBehaviour
{
    public GameObject craftText;
    private Transform textSpawnPos;
    PlayerShoot playerShoot;
    [SerializeField]
    private bool canCraft = false;
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<PlayerShoot>() != null)
            playerShoot = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<PlayerShoot>();
        textSpawnPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (canCraft && Input.GetKeyDown(KeyCode.Space))
        {
            if (playerShoot.CanCraft())
            {
                playerShoot.AddProjetiles(1);
                SpawnText("Projectile +1");
            }
            else
            {
                SpawnText("Full inventory");
            }
        }
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
