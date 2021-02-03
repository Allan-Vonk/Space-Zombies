using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float launchForce = 10;
    bool canJump = true;
    LineRenderer line;
    public Transform target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        line = GetComponent<LineRenderer>();
    }
    void Start()
    {
        line.positionCount = 2;
    }


    void Update()
    {
        Vector2 center = target.position - transform.position;
        var angle = Mathf.Atan2(center.y, center.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        if (Input.GetMouseButtonDown(0) && canJump)
        {
            canJump = false;
            AudioManager.instace.Play("Launch");
            rb.velocity = Vector2.zero;

            Vector2 heading = direction();

            rb.AddForce(heading / heading.magnitude * launchForce, ForceMode2D.Impulse);
        }
        if (canJump)
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, mousePos);
        }
    }

    Vector2 direction()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 heading = mousePos - (Vector2)transform.position;
        return heading;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            AudioManager.instace.Play("Land");
            rb.velocity = Vector2.zero;
            canJump = true;
        }
    }
}
