using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeZombie : ZombieMovement
{

    Camera main;
    void Start()
    {
        main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var dist = Vector2.Distance(transform.position, main.transform.position);
        if(dist < 6)
        {
            main.GetComponent<CameraShake>().ShakeCamera();
        }
    }
}
