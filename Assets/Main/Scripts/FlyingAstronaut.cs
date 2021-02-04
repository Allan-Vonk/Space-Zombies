using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAstronaut : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        rb.AddForce(new Vector2(4, 1), ForceMode2D.Impulse);
    }

    
    void Update()
    {
        rb.rotation = rb.rotation + 28 * Time.deltaTime;
    }

    
}
