using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public GameObject explotion;
    public GameObject gameOverUI;
    SpriteRenderer[] sprites;
    public Color dmgColor;
    private void Awake()
    {
        var obj = GameObject.Find("GameOverUi");
        if (obj != null)
        {
            explotion.GetComponent<ChangeSceneAfter>().gameOVerui = obj;
            obj.SetActive(false);
        }
        var hawqeh = GetComponentsInChildren<SpriteRenderer>();
        sprites = hawqeh;
    }
    public override void Start()
    {
        base.Start();
    }

    public void Update()
    {

    }

    protected override void CheckHealth()
    {

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Kill();
        }

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

    }

    public override void Kill()
    {
        base.Kill();
        Instantiate(explotion, transform.position, explotion.transform.rotation);
        gameOverUI.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            if (canTakeDmg)
            {
                AudioManager.instace.Play("Pdmg");
                canTakeDmg = false;
                StartCoroutine(delay());
                ChangeHealth(-1f);
            }
        }
    }

    bool canTakeDmg = true;
    IEnumerator delay()
    {
        foreach (SpriteRenderer s in sprites)
        {
            s.color = Color.red;
        }
        yield return new WaitForSeconds(0.5f);
        foreach (SpriteRenderer s in sprites)
        {
            s.color = dmgColor;
        }
        canTakeDmg = true;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public override float GetCurrentHealth()
    {
        return currentHealth;
    }
}
