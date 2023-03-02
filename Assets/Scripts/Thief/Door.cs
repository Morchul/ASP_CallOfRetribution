using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool Open { get; private set; }
    
    private Lock doorLock;

    private void Awake()
    {
        doorLock = GetComponent<Lock>();
        Debug.Log("DoorLock: " + doorLock);
    }

    public void Interact()
    {
        if (doorLock == null || !doorLock.Locked)
        {
            if (Open)
                CloseDoor();
            else
                OpenDoor();
        }
        else
        {
            Debug.Log("Door is locked");
        }
    }

    private void CloseDoor()
    {
        Debug.Log("Close door");
        transform.eulerAngles = Vector3.zero;
        Open = false;
    }

    private void OpenDoor()
    {
        Debug.Log("Open door");
        transform.eulerAngles = new Vector3(0,90,0);
        Open = true;
    }
}
