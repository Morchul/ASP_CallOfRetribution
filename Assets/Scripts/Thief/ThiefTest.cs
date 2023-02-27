using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefTest : PositionSensor
{
    [SerializeField]
    private ElectricalLock eLock;

    [SerializeField]
    private BugUpdateEvent onBugUpdate;

    [SerializeField]
    private int bugID;

    [SerializeField]
    private float posUpdateInterval;
    private float posUpdateTimer;

    private void Awake()
    {
        UpdateCreateFunc = MessageUtility.CreateThiefPosMessage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            eLock.PlaceBug(bugID);
            NetworkManager.Instance.Transmitter.WriteToHost(MessageUtility.CreateBugUpdateMessage(bugID, eLock.ObjectType, eLock.State));
        }
        if (posUpdateTimer > 0)
            posUpdateTimer -= Time.deltaTime;
        else
        {
            SendPosUpdate();
            posUpdateTimer = posUpdateInterval;
        }
    }
}
