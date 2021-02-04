using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Grid grid;

    public GameObject[] roomPrefabs = new GameObject[9];
    public GameObject activeRoom;

    public Unit[] enemies = null;
    public GameObject player;

    private DoorManager doorManager;
    private void Start ()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);
        activeRoom = FindObjectOfType<DoorManager>().gameObject;
        ResetDoorManager();
        LoadNewRoom(0);
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
    public int GetOpposingDoorIndex(int index, DoorManager dm)
    {
        switch (index)
        {
            case 0:
            if (dm.Doors[2]) return 2;
            break;
            case 1:
            if (dm.Doors[3]) return 3;
            break;
            case 2:
            if (dm.Doors[0]) return 0;
            break;
            case 3:
            if (dm.Doors[1]) return 1;
            break;
        }
        return 20;
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
        List<GameObject>roomprefabs = GetViableRooms(doorID);
        activeRoom = Instantiate(roomprefabs[Random.Range(0, roomprefabs.Count)]);
        grid.SpawnDebris();
        ResetDoorManager();
        SetDoorState(false);

        //Select a room to spawn
        //Check for viable rooms with door position
        //DoorManager doormanager = roomprefabs[Random.Range(0,roomprefabs.Count)].GetComponent<DoorManager>();

        player.transform.position = doorManager.Doors[GetOpposingDoorIndex(doorID,doorManager)].transform.position;
        grid.CreateGrid();
    }
    List<GameObject> GetViableRooms (int doorID)
    {
        List<GameObject> viablerooms = new List<GameObject>();
        foreach (GameObject Item in roomPrefabs)
        {
            DoorManager doormanager = Item.GetComponent<DoorManager>();
            int index = GetOpposingDoorIndex(doorID,doormanager);
            if (index != 20)
            {
                viablerooms.Add(Item);
            }
        }
        return viablerooms;
    }
}
