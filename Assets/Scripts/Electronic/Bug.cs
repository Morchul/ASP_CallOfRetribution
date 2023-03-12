using System.Collections;
using UnityEngine;

public class Bug : ElectronicDevice
{
    [SerializeField]
    private BugUpdateEvent onBugUpdateEvent;
    [SerializeField]
    private BugUpdateEvent onBugUpdateRequestEvent;
    [SerializeField]
    private IntEvent onBugDisturbedEvent;
    [SerializeField]
    private IntEvent onBugDeniedEvent;

    private Vector3 defaultScale;

    public int BugID { get; private set; }
    private IBugable placedOnItem;

    public void Init(int bugID)
    {
        this.BugID = bugID;
    }

    private void Awake()
    {
        onBugUpdateRequestEvent.AddListener(StateUpdate);
        gameObject.SetActive(false);
        defaultScale = transform.localScale;
    }

    private void StateUpdate(int bugID, IBugable.Type type, int state)
    {
        if (this.BugID != bugID || placedOnItem == null) return;

        if (Disturbed)
        {
            onBugDisturbedEvent.RaiseEvent(bugID);
            return;
        }
        if (placedOnItem.TryChangeState(state))
        {
            onBugUpdateEvent.RaiseEvent(BugID, placedOnItem.ObjectType, placedOnItem.State);
        }
        else
        {
            onBugDeniedEvent.RaiseEvent(bugID);
        }
    }

    protected override void AfterDisturbedChange()
    {
        if(!Disturbed)
            onBugUpdateEvent.RaiseEvent(BugID, placedOnItem.ObjectType, placedOnItem.State);
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
            onBugUpdateEvent.RaiseEvent(BugID, placedOnItem.ObjectType, placedOnItem.State);
        }
    }

    public void RemoveBy(Thief thief)
    {
        placedOnItem.PlaceBug(-1);
        placedOnItem = null;

        if (!Disturbed)
            onBugUpdateEvent.RaiseEvent(BugID, IBugable.Type.None, 0);
        transform.parent = thief.transform;
        transform.localScale = defaultScale;
        thief.ReceiveABug(this);
        gameObject.SetActive(false);
    }
}
