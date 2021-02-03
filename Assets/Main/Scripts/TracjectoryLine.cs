using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracjectoryLine : MonoBehaviour
{
    public LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void RenderLine(Vector2 startPos, Vector2 endPos)
    {
        lr.positionCount = 2;
        Vector3[] points = new Vector3[2];
        points[0] = startPos;
        points[1] = endPos;

        lr.SetPositions(points);
    }

    public void DisableLine() 
    {
        lr.positionCount = 0;
    }
}
