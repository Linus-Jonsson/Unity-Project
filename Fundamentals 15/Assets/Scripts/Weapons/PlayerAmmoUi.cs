using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAmmoUi : MonoBehaviour
{
    public Image image;
    public float xDist;
    private List<Image> images = new List<Image>();
    private PlayerShoot playerShoot;
    void Start()
    {
        playerShoot = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetComponent<PlayerShoot>();
        for (int i = 0; i < playerShoot.maxProjectiles; i++)
        {
            Image spruta = Instantiate(image, GetComponent<RectTransform>().position + Vector3.right * xDist * i, image.rectTransform.rotation);
            spruta.rectTransform.SetParent(GetComponent<RectTransform>());
            images.Add(spruta);
        }
    }
    public void AddOneUi()
    {   
        float dist = xDist * (images.Count);
        Image spruta = Instantiate(image, GetComponent<RectTransform>().position + Vector3.right * dist, image.rectTransform.rotation);
        spruta.rectTransform.SetParent(GetComponent<RectTransform>());
        images.Add(spruta);
    }
    public void RemoveOneUi()
    {
        int n = images.Count - 1;
        Destroy(images[n]);
        images.Remove(images[n]);
    }
}
