using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{

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
        //Play explosion after rotation
        Debug.Log("YOU DEAD");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            ChangeHealth(-1f);
        }
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
