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

        //rotation
        Vector2 center = rotationTarget.position - transform.position;
        var angle = Mathf.Atan2(center.y, center.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
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
            }

            if (Input.GetMouseButtonUp(0))
            {
                endPos = cam.ScreenToWorldPoint(Input.mousePosition);
                rb.velocity = Vector2.zero;

                force = new Vector2(Mathf.Clamp(startPos.x - endPos.x, minPower.x, maxPower.x), Mathf.Clamp(startPos.y - endPos.y, minPower.y, maxPower.y));
                rb.AddForce(force.normalized * power, ForceMode2D.Impulse);

                fuel.UseFuel(10);
                tl.DisableLine();

                Debug.Log(fuel.GetCurrentFuel());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
            rb.velocity = Vector2.zero;
    }

    float timeToNextFuel = 0;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (timeToNextFuel <= Time.time)
            {
                timeToNextFuel = Time.time + 1;
                fuel.AddFule(5);
            }
        }
    }
}
