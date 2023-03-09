using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool Open { get; private set; }
    
    [SerializeField]
    private Lock doorLock;

    [SerializeField]
    private Transform pivot;

    private void Awake()
    {
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
        transform.RotateAround(pivot.position, Vector3.up, -90);
        Open = false;
    }

    private void OpenDoor()
    {
        Debug.Log("Open door");
        transform.RotateAround(pivot.position, Vector3.up, 90);
        Open = true;
    }
}
