using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesTest : MonoBehaviour
{
    PlayerHealth playerHealth;

    [SerializeField] GameObject[] lightPrefabOn;
    [SerializeField] GameObject[] lightPrefabOff;

    [SerializeField] GameObject lightParent;
    [SerializeField] Sprite brokenLight;
    [SerializeField] Sprite Light;
    [SerializeField] List<Image> lightImages = new List<Image>();

    Vector3 offSet;

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
            //GameObject light = Instantiate(lightPrefabOn[0], lightParent.transform);
            GameObject light = Instantiate(lightPrefabOn[0], offSet = new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
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
            if (i < playerHealth.GetCurrentHealth())
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