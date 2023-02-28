using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : ElectronicDevice
{
    [SerializeField]
    private BugUpdateEvent bugUpdateEvent;

    private int bugID;
    private IBugable placedOnItem;

    public void Init(int bugID)
    {
        this.bugID = bugID;
    }

    private void Awake()
    {
        bugUpdateEvent.AddListener(StateUpdate);
    }

    private void StateUpdate(int bugID, IBugable.Type type, int state)
    {
        if (this.bugID != bugID || placedOnItem == null) return;

        placedOnItem.State = state;
    }

    public void PlaceOn(IBugable bugable)
    {
        placedOnItem = bugable;
        placedOnItem.PlaceBug(bugID);
        NetworkManager.Instance.Transmitter.WriteToHost(MessageUtility.CreateBugUpdateMessage(bugID, placedOnItem.ObjectType, placedOnItem.State));
    }

    public void Remove()
    {
        placedOnItem.PlaceBug(-1);
        placedOnItem = null;
        NetworkManager.Instance.Transmitter.WriteToHost(MessageUtility.CreateBugUpdateMessage(bugID, IBugable.Type.None, 0));
    }
}
