using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeapon : MonoBehaviour
{
    public bool tapFire = true;
    public Transform shootPoint;
    public float launchForce;
    public float aliveTime;
    public float fireRate = 0.2f;
    private float nextFire = 0;

    public float minPitch = 0.7f;
    public float maxPitch = 1.8f;

    public GameObject muzzleFlash;
    public GameObject bullet;
    public AudioClip shootSound;

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        //optimisera?
        if (tapFire)
        {
            if (Input.GetMouseButtonDown(1) && Time.time > nextFire)
                Base();
        }
        else 
        {
            if(Input.GetMouseButton(1) && Time.time > nextFire)
                Base();
        }
    }

    protected virtual void Base()
    {
        nextFire = Time.time + fireRate;
        Shoot();
        PlayAudioClip(shootSound);
        GameObject muzzleClone = Instantiate(muzzleFlash, shootPoint.position, muzzleFlash.transform.rotation);
    }
    protected virtual void Shoot()
    {

    }

    protected virtual void SetVelocity(GameObject bullet)
    {
        StartCoroutine(bullet.GetComponent<NormalBullet>().ExplodeBullet(aliveTime));

        Rigidbody2D cloneRb = bullet.GetComponent<Rigidbody2D>();

        //tyckte detta va smart. Stoppar att de åker igenom saker ifall man skjuter för snabbt
        if (launchForce >= 10)
            cloneRb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        cloneRb.velocity = bullet.transform.up.normalized * launchForce; //transform.up kan va vad som.
    }

    protected void PlayAudioClip(AudioClip clip, bool pitch = true)
    {
        audioSource.clip = clip;
        if (pitch)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
        }
        audioSource.Play();
    }
}
