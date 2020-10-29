using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerShoot : MonoBehaviour
{
    //Sprutvapnet
    public GameObject shootEffekt;
    public GameObject bullet;
    public float launchForce;
    public Transform shootPoint;

    [Range(0.2f, 1f)]
    public float fireRate = 0.5f;
    private float nextFire = 0;

    public int maxProjectiles = 6;
    private int currentAmountOfProjectiles;

    public AudioClip shootSound;
    AudioSource audioSource;
    PlayerAmmoUi ammoUi;
    private void Start()
    {
        currentAmountOfProjectiles = maxProjectiles;
        audioSource = GetComponent<AudioSource>();
        ammoUi = GameObject.FindGameObjectWithTag("AmmoUi").GetComponent<PlayerAmmoUi>();
    }

    void Update()
    {
        if (currentAmountOfProjectiles > 0 && Time.time > nextFire && Input.GetMouseButtonDown(0))
        {
            nextFire = fireRate + Time.time;
            currentAmountOfProjectiles -= 1;
            ammoUi.RemoveOneUi();
            audioSource.clip = shootSound;
            audioSource.Play();

            GameObject shootEffektClone = Instantiate(shootEffekt, shootPoint.position, shootEffekt.transform.rotation);
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bulletClone = Instantiate(bullet, shootPoint.position, shootPoint.rotation);

        Rigidbody2D cloneRb = bulletClone.GetComponent<Rigidbody2D>();

        if (launchForce >= 10)
            bulletClone.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        cloneRb.velocity = bulletClone.transform.up.normalized * launchForce;

    }

    public void AddProjetiles(int amount)
    {
        currentAmountOfProjectiles += amount;
        ammoUi.AddOneUi();
    }

    public int GetCurrentAmount()
    {
        return currentAmountOfProjectiles;
    }

    public bool CanCraft()
    {
        if (currentAmountOfProjectiles >= maxProjectiles)
        {
            currentAmountOfProjectiles = maxProjectiles;
            return false;
        }
        return true;
    }
}
