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
    }

    private void CloseDoor()
    {
        Open = false;
    }

    private void OpenDoor()
    {
        Open = true;
    }
}
