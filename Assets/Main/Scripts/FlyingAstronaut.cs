using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAstronaut : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(transform.position.x, Random.Range(381, 30), 0);
        rb.AddForce(new Vector2(4, 1), ForceMode2D.Impulse);
    }

    
    void Update()
    {
        rb.rotation = rb.rotation + 28 * Time.deltaTime;
    }

    
}
