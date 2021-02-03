using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToShoot : MonoBehaviour
{
    public float power = 5;

    public Vector2 minPower;
    public Vector2 maxPower;

    Vector2 force;
    Vector2 startPos;
    Vector2 endPos;

    bool refuel;

    Camera cam;
    Rigidbody2D rb;
    TracjectoryLine tl;
    Fuel fuel;
    public Transform rotationTarget;
    public LayerMask ignoreMask;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        cam = Camera.main;
        tl = GetComponent<TracjectoryLine>();
        fuel = GetComponentInChildren<Fuel>();
    }

    //Different fuel usage on how far you drag arrow!
    void Update()
    {
        DragAndShoot();


        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 3f, ~ignoreMask);
        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            Vector2 center = hit.point - (Vector2)transform.position;
            var angle = Mathf.Atan2(center.y, center.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }
    }

    Vector2 halfVel;
    void DragAndShoot()
    {
        if (fuel.GetCurrentFuel() > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = cam.ScreenToWorldPoint(Input.mousePosition);
                halfVel = rb.velocity.normalized * 0.3f * power;
            }

            if (Input.GetMouseButton(0))
            {
                Vector2 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                tl.RenderLine(startPos, currentPoint);
                rb.velocity = halfVel;

                Vector2 center = currentPoint - startPos;
                var angle = Mathf.Atan2(center.y, center.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
                //StartCoroutine(Rotate(angle + 90));
            }

            if (Input.GetMouseButtonUp(0))
            {
                endPos = cam.ScreenToWorldPoint(Input.mousePosition);
                rb.velocity = Vector2.zero;

                float forceMutli = Vector2.Distance(startPos, endPos);
                forceMutli = Mathf.Clamp(forceMutli, 0, 2);
                force = new Vector2(Mathf.Clamp(startPos.x - endPos.x, minPower.x, maxPower.x), Mathf.Clamp(startPos.y - endPos.y, minPower.y, maxPower.y));
                rb.AddForce(force.normalized * power * forceMutli, ForceMode2D.Impulse);

                fuel.UseFuel(10);
                tl.DisableLine();

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            rb.velocity = Vector2.zero;
            halfVel = Vector2.zero;
        }
    }

    float timeToNextFuel = 0;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            if (timeToNextFuel <= Time.time)
            {
                timeToNextFuel = Time.time + 1;
                fuel.AddFule(5);
            }
        }
    }
}
