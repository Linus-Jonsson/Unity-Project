using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] int health = 1;
    [SerializeField] GameObject deathVFX; // ToDo - Create/add deathVFX

    public void DealDamage(int damage)
    {
        health -= damage;
        if (health <= 0) {
            TriggerDeathVFX();
            // ToDo - Function that makes zombie into human
        }
    }

    private void TriggerDeathVFX()
    {
        if (!deathVFX)
            return;
        GameObject deathVFXObject = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(deathVFXObject, 1f);
    }
}
