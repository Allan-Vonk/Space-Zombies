using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDmgTrail : MonoBehaviour
{
    public float dmgRange = 4f;
    public float boxSize = 1f;
    public LayerMask ignoreMe;

    public RaycastHit2D ray;

    public bool canDmg;
    public bool aiming;
    public GameObject launchParticle;
    private GameObject mainParticle;
    public GameObject flyingEffekt;
    public GameObject idelEffekt;
    public GameObject aimEffekt;

    GameObject clone;
    GameObject clone2;
    GameObject clone3;
    private void Awake()
    {
        mainParticle = transform.GetChild(0).gameObject;

        //when u need sleep and food
        clone = Instantiate(flyingEffekt, mainParticle.transform.position, mainParticle.transform.rotation);
        clone2 = Instantiate(aimEffekt, mainParticle.transform.position, mainParticle.transform.rotation);
        clone3 = Instantiate(idelEffekt, mainParticle.transform.position, mainParticle.transform.rotation);

        clone.transform.SetParent(transform);
        clone2.transform.SetParent(transform);
        clone3.transform.SetParent(transform);

        clone.SetActive(false);
        clone2.SetActive(false);
        clone3.SetActive(false);

    }
    void Update()
    {
        if (canDmg)
        {
            RayCast();
            FlyingEffekts();
        }
        else if (aiming)
        {
            AimEffekt();
        }
        else
        {
            wallEffekt();
        }
    }

    public void FlyingEffekts()
    {
        clone.SetActive(true);
        clone2.SetActive(false);
        clone3.SetActive(false);
    }

    public void AimEffekt()
    {
        clone.SetActive(false);
        clone2.SetActive(true);
        clone3.SetActive(false);
    }

    public void wallEffekt()
    {
        clone.SetActive(false);
        clone2.SetActive(false);
        clone3.SetActive(true);
    }

    public void LaunchEffekt()
    {
        GameObject launchCLone = Instantiate(launchParticle, mainParticle.transform.position, launchParticle.transform.rotation);
        Destroy(launchCLone, 2f);
    }

    public void RayCast()
    {
        ray = Physics2D.BoxCast(transform.position, new Vector2(boxSize, boxSize), 0f, transform.up * -1, dmgRange, ~ignoreMe);
        Debug.DrawRay(transform.position, transform.up * -1 * dmgRange, Color.red);
        if (ray.collider != null && ray.collider.CompareTag("Zombie"))
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
