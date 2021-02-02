using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Grid grid;

    public GameObject[] roomPrefabs = new GameObject[9];

    public Door[] doors = new Door[4];
    private GameObject activeRoom;

    public GameObject player;
    private void Start ()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);

        LoadNewRoom(1);
    }
    private void Update ()
    {
        
    }
    public Vector3 GetOpposingDoorVector(int index)
    {
        switch (index)
        {
            case 0:
            return doors[2].transform.position;
            break;
            case 1:
            return doors[3].transform.position;
            break;
            case 2:
            return doors[0].transform.position;
            break;
            case 3:
            return doors[1].transform.position;
            break;
        }
        return doors[0].transform.position;
    }
    public void SetDoorState (bool state)
    {
        foreach (Door Item in doors)
        {
            Item.openState = state;
        }
    }
    public void LoadNewRoom (int doorID)
    {
        //SetDoorState(false);
        player.transform.position = GetOpposingDoorVector(doorID);
        Destroy(activeRoom);
        activeRoom = Instantiate(roomPrefabs[Random.Range(0, 9)]);
        grid.CreateGrid();
    }
}
