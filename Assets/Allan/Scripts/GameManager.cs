using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Grid grid;

    public GameObject[] roomPrefabs = new GameObject[9];
    private GameObject activeRoom;

    public Unit[] enemies = null;
    public GameObject player;

    private DoorManager doorManager;
    private void Start ()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);

        LoadNewRoom(1);
        grid.SpawnDebris();
    }
    private void Update ()
    {
        enemies = CheckForUnitsInScene();
        ResetDoorManager();
        CheckAndSetDoorState();
    }
    public Unit[] CheckForUnitsInScene ()
    {
        return FindObjectsOfType<Unit>();
    }
    public void CheckAndSetDoorState ()
    {
        if (enemies != null) 
        {
            bool check = (enemies.Length >0) ? false : true;
            SetDoorState(check);
        }
    }
    public int GetOpposingDoorIndex(int index)
    {
        switch (index)
        {
            case 0:
            if(doorManager.Doors[2])return 2;
            break;
            case 1:
            if (doorManager.Doors[3]) return 3;
            break;
            case 2:
            if (doorManager.Doors[0]) return 0;
            break;
            case 3:
            if (doorManager.Doors[1]) return 1;
            break;
        }
        return 0;
    }
    public void SetDoorState (bool state)
    {
        foreach (Door Item in doorManager.Doors)
        {
            if(Item)Item.openState = state;
        }
    }
    public void ResetDoorManager ()
    {
        doorManager = FindObjectOfType<DoorManager>();
    }
    public void LoadNewRoom (int doorID)
    {
        Destroy(activeRoom);
        activeRoom = Instantiate(roomPrefabs[Random.Range(0, 9)]);
        ResetDoorManager();
        SetDoorState(false);

        //Select a room to spawn
        //Check for viable rooms with door position
        List<GameObject>roomprefabs = GetViableRooms(doorID);
        DoorManager doormanager = roomprefabs[Random.Range(0,roomprefabs.Count)].GetComponent<DoorManager>();

        player.transform.position = doormanager.Doors[GetOpposingDoorIndex(doorID)].transform.position;
        grid.CreateGrid();
    }
    List<GameObject> GetViableRooms (int doorID)
    {
        List<GameObject> viablerooms = new List<GameObject>();
        foreach (GameObject Item in roomPrefabs)
        {
            DoorManager doormanager = Item.GetComponent<DoorManager>();
            Door door = doormanager.Doors[GetOpposingDoorIndex(doorID)];
            if (door)
            {
                viablerooms.Add(Item);
            }
        }
        return viablerooms;
    }
}
