using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int doorID;
    public string playerTag;
    public bool openState;
    private void OnCollisionEnter (Collision collision)
    {
        if (openState)
        {
            if (collision.gameObject.CompareTag(playerTag)) GameManager.instance.LoadNewRoom(doorID);
        }
        Debug.Log("Colliding with door");
    }
}
