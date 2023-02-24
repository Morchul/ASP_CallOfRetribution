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
        Open = false;
    }

    private void OpenDoor()
    {
        Debug.Log("Open door");
        Open = true;
    }
}
