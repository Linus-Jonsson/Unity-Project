using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

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
    private static bool canPickUp = true;
    private bool isInPlayerHands = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        //optimisera?
        if (tapFire)
        { 
            if (isInPlayerHands && Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
                Base();
        }
        else
        {
            if (isInPlayerHands && Input.GetKey(KeyCode.Space) && Time.time > nextFire)
                Base();
        }
    }

    protected virtual void Base()
    {
        nextFire = Time.time + fireRate;
        Shoot();
        PlayAudioClip(shootSound);
        GameObject muzzleClone = Instantiate(muzzleFlash, shootPoint.position, muzzleFlash.transform.rotation);
        muzzleClone.transform.parent = shootPoint.transform;
    }
    protected virtual void Shoot()
    {

    }

    protected virtual void SetVelocity(GameObject bullet)
    {
        bullet.GetComponent<NormalBullet>().ExplodeAfter(aliveTime);
        Rigidbody2D cloneRb = bullet.GetComponent<Rigidbody2D>();
        cloneRb.velocity = bullet.transform.up.normalized * launchForce;
        //tyckte detta va smart. Stoppar att de åker igenom saker ifall man skjuter för snabbt
        if (launchForce >= 10)
            cloneRb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (canPickUp)
            {
                canPickUp = false;
                StartCoroutine(PickUpDelay(1));
                var newPos = GameObject.FindGameObjectWithTag("WeaponPoint2").transform;
                var pos = gameObject.transform;

                if (newPos.childCount > 0)
                {
                    Transform child = newPos.GetChild(0);
                    child.transform.position = pos.position;
                    child.transform.rotation = pos.rotation;
                    child.transform.parent = null;
                    child.GetComponent<ShootWeapon>().isInPlayerHands = false;
                    child.GetComponent<Collider2D>().enabled = true;
                    ChangeWeapon(newPos);
                }
                else
                {
                    ChangeWeapon(newPos);                    
                }
            }
        }
    }
    void ChangeWeapon(Transform newTransform)
    {
        gameObject.transform.position = newTransform.position;
        gameObject.transform.rotation = newTransform.rotation;
        gameObject.transform.parent = newTransform.transform;
        isInPlayerHands = true;
        GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator PickUpDelay(float time)
    {
        yield return new WaitForSeconds(time);
        canPickUp = true;
    }
}
