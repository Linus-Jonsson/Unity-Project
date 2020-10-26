using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
	public float swingSpeed = 6;
	public float swingAngle = 90;

    public AudioClip swordWing;
    public GameObject zombieSlashEffect;
    private AudioSource audioSource;

	bool isSwinging = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && !isSwinging)
        {
            audioSource.clip = swordWing;
            audioSource.Play();
			StartCoroutine(Swong(swingSpeed, swingAngle));
		}
	}

    bool swingRight = true;
    //lmao kanske borde animeras men blev triggrad som satan på unity animator så gjorde animationen i kod :D
    IEnumerator Swong(float swingSpeed, float swingAngle = 90)
    {
        isSwinging = true;

        var point1 = transform.forward * swingAngle;
        var point2 = transform.forward * -swingAngle;

        if (!swingRight)
        {
            var tempPoint = point1;
            point1 = point2;
            point2 = tempPoint;
            swingRight = true;
        }
        else
            swingRight = false;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * swingSpeed;
            transform.localEulerAngles = Vector3.Lerp(point1, point2, t);
            yield return null;
        }
        isSwinging = false;
        yield return new WaitForEndOfFrame();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag;
       
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            GameObject zombieEffekt = Instantiate(zombieSlashEffect, pos, rot);
        }
    }
}
