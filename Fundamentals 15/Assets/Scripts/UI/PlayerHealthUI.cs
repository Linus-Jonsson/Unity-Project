using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Image image;
    public Image backgroundHeart;
    public float xDist;
    private List<Image> images = new List<Image>();
    private PlayerHealth playerHealth;
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        for (int i = 0; i < playerHealth.startHealth; i++)
        {
            Image backgrHeart = Instantiate(backgroundHeart, GetComponent<RectTransform>().position + Vector3.right * xDist * i, image.rectTransform.rotation);
            backgrHeart.rectTransform.SetParent(GetComponent<RectTransform>());
        }
        for (int i = 0; i < playerHealth.startHealth; i++)
        {
            Image spruta = Instantiate(image, GetComponent<RectTransform>().position + Vector3.right * xDist * i, image.rectTransform.rotation);
            spruta.rectTransform.SetParent(GetComponent<RectTransform>());
            images.Add(spruta);
        }
    }

    public void RemoveFromUi(int damage)
    {
        int n = images.Count - damage;
        Destroy(images[n]);
        images.Remove(images[n]);
    }
}
