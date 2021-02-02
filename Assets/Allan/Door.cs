using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int doorID;
    public string playerTag;
    public bool openState;

    private void OnTriggerEnter (Collider other)
    {
        if (openState == true)
        {
            Debug.Log("Colliding with door");
            if (other.gameObject.CompareTag(playerTag)) GameManager.instance.LoadNewRoom(doorID);
        }
    }
}
