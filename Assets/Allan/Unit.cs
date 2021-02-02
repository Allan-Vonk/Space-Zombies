using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float speed = 1;
    public float checkRadius = .5f;
    private Queue<Vector3> path;
    private Vector3 lastPos = new Vector3();
    public Transform target;
    private Pathfinding pathfinding;
    private void Start ()
    {
        pathfinding = FindObjectOfType<Pathfinding>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update ()
    {
        if (target.position != lastPos)
        {
            lastPos = target.position;
            path = pathfinding.FindPath(transform.position, target.position);
        }
    }
    private void FixedUpdate ()
    {
        if (path != null && path.Count > 0)
        {
            if (Vector3.Distance(transform.position, path.Peek()) > checkRadius) MoveToNextPosition();
            else path.Dequeue();
        }
    }
    private void MoveToNextPosition ()
    {
        Vector3 dir =  (path.Peek() - transform.position).normalized;
        Debug.Log(dir);
        transform.position += dir * speed;
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
