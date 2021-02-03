using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    PlayerHealth playerHealth;

    [SerializeField] GameObject lightPrefab;
    [SerializeField] GameObject lightParent;
    [SerializeField] Sprite brokenLight;
    [SerializeField] Sprite Light;
    [SerializeField] List<Image> lightImages = new List<Image>();

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();

        //Checks if there is a playerhealth script.
        if (playerHealth == null)
        {
            Debug.LogError("Can't find PlayerHealth!");
            return;
        }

        //Places the prefabs of the lights in the parent and draws them in the lives ui.
        for (int i = 0; i < playerHealth.GetMaxHealth(); i++)
        {
            GameObject light = Instantiate(lightPrefab, lightParent.transform);
            lightImages.Add(light.GetComponent<Image>());
        }
    }

    void Update()
    {
        LivesUpdate();
    }

    public void LivesUpdate()
    {
        //Checks what light needs to be drawn.
        for (int i = 0; i < lightImages.Count; i++)
        {
            if(i < playerHealth.GetCurrentHealth())
            {
                lightImages[i].sprite = Light;
            }
            else
            {
                lightImages[i].sprite = brokenLight;
            }
        }
    }
}
