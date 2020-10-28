using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstRifle : ShootWeapon
{
    public int amountOfBullets;
    protected override void Shoot()
    {
        StartCoroutine(FireBurst(bullet, amountOfBullets, fireRate/(float)amountOfBullets));
    }
    public IEnumerator FireBurst(GameObject bulletPrefab, int burstSize, float rateOfFire)
    {
        float bulletDelay = rateOfFire;
        Vector3 point = shootPoint.transform.position;
        for (int i = 0; i < burstSize; i++)
        {
            if (i > 0)
                PlayAudioClip(shootSound);

            Instantiate(muzzleFlash, point, muzzleFlash.transform.rotation);
            GameObject bulletClone = Instantiate(bulletPrefab, point, transform.rotation);
            SetVelocity(bulletClone);


            yield return new WaitForSeconds(bulletDelay);
        }
    }
}
