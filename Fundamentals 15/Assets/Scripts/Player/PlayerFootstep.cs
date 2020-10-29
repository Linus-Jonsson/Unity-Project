using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstep : MonoBehaviour
{
    public AudioClip[] footSteps;
    AudioSource audioSource;
    Rigidbody2D rb;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude > 0.1f && audioSource.isPlaying == false)
        {
            int r = Random.Range(0, footSteps.Length);
            audioSource.clip = footSteps[r];
            audioSource.Play();
        }        
    }
}
