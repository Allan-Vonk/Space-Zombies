using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombie;
    public int amountOfZombies = 3;
    public float roomSizeX;
    public float roomSizeY;

    Vector3 center;
    Vector3 topLeft;
    Vector3 botRight;
    void Awake()
    {
        center = transform.position;
        float cx = center.x;
        float cy = center.y;

        topLeft.x = -roomSizeX * 0.5f + cx;
        topLeft.y = roomSizeY * 0.5f + cy;

        botRight.x = roomSizeX * 0.5f + cx;
        botRight.y = -roomSizeY * 0.5f + cy;
        for (int i = 0; i < amountOfZombies; i++)
        {
            Instantiate(zombie, zombieSpawnPos(topLeft.x, topLeft.y, botRight.x, botRight.y), Quaternion.identity);
        }
    }

    Vector3 zombieSpawnPos(float leftX, float topY, float rightX, float botY)
    {
        float x = Random.Range(leftX, rightX);
        float y = Random.Range(botY, topY);
        return new Vector3(x, y, 0);
    }
    private void OnDrawGizmos()
    {
        center = transform.position;
        float cx = center.x;
        float cy = center.y;

        topLeft.x = -roomSizeX * 0.5f + cx;
        topLeft.y = roomSizeY * 0.5f + cy;

        botRight.x = roomSizeX * 0.5f + cx;
        botRight.y = -roomSizeY * 0.5f + cy;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(topLeft, botRight);
        //Gizmos.DrawLine(botRight, b);
    }
}
