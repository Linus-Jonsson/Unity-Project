using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SplashTextTimer : MonoBehaviour
{
    [SerializeField] int waitTime = 2;

    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(waitTime);
        GetComponentInChildren<TextMeshProUGUI>().enabled = true;
    }
}
