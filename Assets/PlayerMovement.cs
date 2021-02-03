using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float launchForce = 10;
    bool canJump = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canJump)
        {
            canJump = false;
            rb.velocity = Vector2.zero;

            Vector2 heading = direction();

            var angle = Mathf.Atan2(heading.y, heading.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            rb.AddForce(heading / heading.magnitude * launchForce, ForceMode2D.Impulse);
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
            rb.velocity = Vector2.zero;
            canJump = true;
        }
    }
}
