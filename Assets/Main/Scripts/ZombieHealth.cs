using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public GameObject explotion;
    public int startHealth = 10000;
    public Color blinkColor;
    public float dmgDelay = 1f;

    int currentHeatlh;
    bool canTakeDmg;
    Color startColor;
    SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.color;

        currentHeatlh = startHealth;
        canTakeDmg = true;
    }

    public void TakeDmg(int dmg = 1)
    {
        if (canTakeDmg)
        {
            canTakeDmg = false;
            currentHeatlh -= dmg;
            if (currentHeatlh <= 0)
            {
                Death();
            }
            else
            {
                AudioManager.instace.Play("Zhurt", transform);
                StartCoroutine(Blink());
            }
        }
    }
    void Death()
    {
        AudioManager.instace.Play("Zdeath", transform);
        GameObject explotionClone = Instantiate(explotion, transform.position, explotion.transform.rotation);
        Destroy(explotionClone, 2f);
        ScoreManager.Score += 1f;
        Destroy(gameObject);
    }
    IEnumerator Blink(float t = 0.4f)
    {
        rend.color = blinkColor;
        yield return new WaitForSeconds(t);
        rend.color = startColor;
        yield return new WaitForSeconds(dmgDelay - t);
        canTakeDmg = true;
    }
}
