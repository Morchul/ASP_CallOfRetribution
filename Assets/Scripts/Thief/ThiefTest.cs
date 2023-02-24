using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefTest : MonoBehaviour
{
    [SerializeField]
    private ElectricalLock eLock;

    [SerializeField]
    private BugUpdateEvent onBugUpdate;

    [SerializeField]
    private int bugID;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            eLock.PlaceBug(bugID);
            NetworkManager.Instance.Transmitter.WriteToHost(MessageUtility.CreateBugUpdateMessage(bugID, eLock.ObjectType, eLock.State));
        }

    }
}
