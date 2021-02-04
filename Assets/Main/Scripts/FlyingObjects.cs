using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObjects : MonoBehaviour
{
    public float rotationSpeed;
    public Vector2 force;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        rb.AddForce(new Vector2(force.x, force.y), ForceMode2D.Impulse);
    }

    
    void Update()
    {
        rb.rotation = rb.rotation + rotationSpeed * Time.deltaTime;
    }

    
}
