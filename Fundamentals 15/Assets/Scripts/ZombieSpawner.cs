using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 3f;
    [SerializeField] GameObject zombie;
    bool spawn = true;

    void Start()
    {
        StartCoroutine(ZombieSpawn());
    }

    IEnumerator ZombieSpawn()
    {
        while (spawn) {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnZombie();
        }
    }
    
    void SpawnZombie()
    {
        GameObject newZombie = Instantiate (zombie, transform.position, transform.rotation) as GameObject;
        newZombie.transform.parent = transform;
    }
}
