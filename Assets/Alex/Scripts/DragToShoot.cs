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
    }

    void DragAndShoot()
    {
        if (fuel.GetCurrentFuel() > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = cam.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Vector2 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                tl.RenderLine(startPos, currentPoint);
            }

            if (Input.GetMouseButtonUp(0))
            {
                endPos = cam.ScreenToWorldPoint(Input.mousePosition);

                force = new Vector2(Mathf.Clamp(startPos.x - endPos.x, minPower.x, maxPower.x), Mathf.Clamp(startPos.y - endPos.y, minPower.y, maxPower.y));
                rb.AddForce(force * power, ForceMode2D.Impulse);

                fuel.UseFuel(10);
                tl.DisableLine();

                Debug.Log(fuel.GetCurrentFuel());
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CanLaunch"))
            refuel = true;
        else
            refuel = false;
    }
}
