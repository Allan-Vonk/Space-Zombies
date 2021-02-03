﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float speed = 1;
    public float checkRadius = .5f;
    public float addForceReloadTime = 1f;
    private Queue<Vector3> path;
    private Vector3 lastPos = new Vector3();
    private Rigidbody2D rb;
    public Transform target;
    private Pathfinding pathfinding;
    private void Start ()
    {
        pathfinding = FindObjectOfType<Pathfinding>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        path = pathfinding.FindPath(transform.position, target.position);
        StartCoroutine(AddForceToDirection());
    }
    private void Update ()
    {
        if (target.position != lastPos)
        {
            lastPos = target.position;
            path = pathfinding.FindPath(transform.position, target.position);
        }
        if (path != null && path.Count > 0)
        {
            if (Vector3.Distance(transform.position, path.Peek()) < checkRadius) path.Dequeue();
        }
    }
    private void FixedUpdate ()
    {

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
            rb.AddForce(dir.normalized * speed);
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