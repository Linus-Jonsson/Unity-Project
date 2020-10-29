using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float activeTime = GetComponent<ParticleSystem>().duration;
        StartCoroutine(DestoryItSelf(activeTime));
    }

    IEnumerator DestoryItSelf(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }
}
