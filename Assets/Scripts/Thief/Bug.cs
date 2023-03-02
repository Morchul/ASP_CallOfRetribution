using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : ElectronicDevice
{
    [SerializeField]
    private BugUpdateEvent bugUpdateEvent;

    public int BugID { get; private set; }
    private IBugable placedOnItem;

    public void Init(int bugID)
    {
        this.BugID = bugID;
    }

    private void Awake()
    {
        bugUpdateEvent.AddListener(StateUpdate);
        gameObject.SetActive(false);
    }

    private void StateUpdate(int bugID, IBugable.Type type, int state)
    {
        if (this.BugID != bugID || placedOnItem == null) return;

        placedOnItem.State = state;
    }

    public void PlaceOn(IBugable bugable)
    {
        placedOnItem = bugable;
        placedOnItem.PlaceBug(BugID);
        NetworkManager.Instance.Transmitter.WriteToHost(MessageUtility.CreateBugUpdateMessage(BugID, placedOnItem.ObjectType, placedOnItem.State));
        Transform bugTransform = bugable.GetBugPosition();
        transform.parent = bugTransform;
        transform.position = bugTransform.position;
        transform.rotation = bugTransform.rotation;
        gameObject.SetActive(true);
    }

    public void RemoveBy(ThiefTest thief)
    {
        placedOnItem.PlaceBug(-1);
        placedOnItem = null;
        NetworkManager.Instance.Transmitter.WriteToHost(MessageUtility.CreateBugUpdateMessage(BugID, IBugable.Type.None, 0));
        transform.parent = thief.transform;
        thief.ReceiveABug(this);
        gameObject.SetActive(false);
    }
}
