using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : ElectronicDevice
{
    [SerializeField]
    private BugUpdateEvent bugUpdateEvent;
    [SerializeField]
    private BugUpdateEvent bugUpdateRequestEvent;

    private Vector3 defaultScale;

    public int BugID { get; private set; }
    private IBugable placedOnItem;

    public void Init(int bugID)
    {
        this.BugID = bugID;
    }

    private void Awake()
    {
        bugUpdateRequestEvent.AddListener(StateUpdate);
        gameObject.SetActive(false);
        defaultScale = transform.localScale;
    }

    private void StateUpdate(int bugID, IBugable.Type type, int state)
    {
        if (this.BugID != bugID || placedOnItem == null) return;

        if (Disturbed)
        {
            NetworkManager.Instance.Transmitter.WriteToClient(MessageUtility.BUG_DISTURBED);
            return;
        }
        if (placedOnItem.TryChangeState(state))
        {
            bugUpdateEvent.RaiseEvent(BugID, placedOnItem.ObjectType, placedOnItem.State);
        }
        else
        {
            NetworkManager.Instance.Transmitter.WriteToClient(MessageUtility.BUG_DENIED);
        }
    }

    protected override void AfterDisturbedChange()
    {
        if(!Disturbed)
            bugUpdateEvent.RaiseEvent(BugID, placedOnItem.ObjectType, placedOnItem.State);
    }

    public void PlaceOn(IBugable bugable)
    {
        placedOnItem = bugable;
        placedOnItem.PlaceBug(BugID);

        Transform bugTransform = bugable.GetBugPosition();
        transform.SetParent(bugTransform, true);
        transform.position = bugTransform.position;
        transform.rotation = bugTransform.rotation;
        gameObject.SetActive(true);

        StartCoroutine(SendDelayedBugPlacedUpdate());
        
    }

    private IEnumerator SendDelayedBugPlacedUpdate()
    {
        yield return new WaitForSeconds(1);

        if (!Disturbed)
        {
            bugUpdateEvent.RaiseEvent(BugID, placedOnItem.ObjectType, placedOnItem.State);
        }
    }

    public void RemoveBy(ThiefTest thief)
    {
        placedOnItem.PlaceBug(-1);
        placedOnItem = null;

        if (!Disturbed)
            bugUpdateEvent.RaiseEvent(BugID, IBugable.Type.None, 0);
        transform.parent = thief.transform;
        transform.localScale = defaultScale;
        thief.ReceiveABug(this);
        gameObject.SetActive(false);
    }
}
