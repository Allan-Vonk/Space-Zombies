using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float speed = 1;
    public float checkRadius = .5f;
    public float repathDistance;
    public float addForceReloadTime = 1f;
    public float activationRange = 5f;
    private Queue<Vector3> path;
    private Vector3 lastPos = new Vector3();
    private Rigidbody2D rb;
    public Transform target;
    private Pathfinding pathfinding;

    private float SqrMaxVelocity;
    private float MaxVelocity;

    public bool activated = false;
    private void Start ()
    {
        SetMaxVelocity(1.5f);
        pathfinding = FindObjectOfType<Pathfinding>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        path = pathfinding.FindPath(transform.position, target.position);
        StartCoroutine(AddForceToDirection());
    }
    public void SetMaxVelocity (float maxVelocity)
    {
        this.MaxVelocity = maxVelocity;
        SqrMaxVelocity = maxVelocity * maxVelocity;
    }
    private void Update ()
    {
        if (activated == false&&Vector2.Distance(transform.position,target.position) < activationRange)
        {
            activated = true;
        }
        if (target.position != lastPos)
        {
            lastPos = target.position;
            UpdatePath();
        }
        if (path != null && path.Count > 0)
        {
            if (Vector3.Distance(transform.position, path.Peek()) > repathDistance) UpdatePath();
            if (Vector3.Distance(transform.position, path.Peek()) < checkRadius) path.Dequeue();
        }
    }
    public void UpdatePath ()
    {
        path = pathfinding.FindPath(transform.position, target.position);
    }
    private void FixedUpdate ()
    {
        Vector2 V = rb.velocity;
        if (V.sqrMagnitude > SqrMaxVelocity)
        {
            rb.velocity = V.normalized * MaxVelocity;
        }
    }
    private void MoveToNextPosition ()
    {
        Vector3 dir =  (path.Peek() - transform.position).normalized;
        transform.position += dir * speed;
    }
    IEnumerator AddForceToDirection ()
    {
        if (path!= null&&path.Count >0)
        {
            Debug.Log("Adding force");
            Vector3 dir =  (path.Peek() - transform.position).normalized;
            if (activated == true)
            {
                rb.AddForce(dir.normalized * speed);
            }
            yield return new WaitForSeconds(addForceReloadTime);
            StartCoroutine(AddForceToDirection());
        }
        else
        {
            yield return new WaitForSeconds(addForceReloadTime);
            StartCoroutine(AddForceToDirection());
        }
    }
    private void OnDrawGizmos ()
    {
        Gizmos.color = (activated) ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, activationRange);
        if (path != null&&path.Count > 0)
        {
            foreach (Vector3 vector3 in path)
            {
                Gizmos.color = (vector3 == path.Peek()) ? Color.blue : Color.red;
                Gizmos.DrawWireSphere(vector3 + new Vector3(0,1,0),.1f);
            }
        }

    }
}
