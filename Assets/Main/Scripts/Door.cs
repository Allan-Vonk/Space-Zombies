using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int doorID;
    public string playerTag;
    public bool openState;

    private void OnTriggerEnter2D (Collider2D collision)
    {
        Debug.Log("Loading new room");
        if (openState == true)
        {
            Debug.Log("Colliding with door");
            if (collision.gameObject.CompareTag(playerTag)) GameManager.instance.LoadNewRoom(doorID);
        }
    }
}
