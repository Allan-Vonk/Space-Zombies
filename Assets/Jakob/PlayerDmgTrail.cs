using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDmgTrail : MonoBehaviour
{
    public float dmgRange = 4f;
    public float boxSize = 1f;

    public RaycastHit2D ray;
    void Update()
    {
        ray = Physics2D.BoxCast(transform.position, new Vector2(boxSize, boxSize), 0f, transform.up * -1, dmgRange);
        Debug.DrawRay(transform.position, transform.up * -1 * dmgRange, Color.red);
        if(ray.collider != null && ray.collider.CompareTag("Zombie"))
        {
            HitZombie(ray.collider.gameObject);
        }
    }
    public void HitZombie(GameObject zombie)
    {
        zombie.GetComponent<ZombieHealth>().TakeDmg();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(boxSize, boxSize, boxSize));
    }
}
