using UnityEngine;

public class Alarm : MonoBehaviour, IBugable
{
    
    [SerializeField]
    private Transform bugPosition;

    [SerializeField]
    private float alarmRadius;

    [SerializeField]
    private LayerMask guardLayer;

    public bool Disabled { get; private set; }

    protected void Awake()
    {
        bugID = -1;
        Disabled = false;
    }

    public void Interact()
    {
        if (!Disabled) State = (int)(state | AlarmState.On);
    }

    public void SetOff()
    {
        Debug.Log("Set off alarm");
        foreach(Collider collider in Physics.OverlapSphere(transform.position, alarmRadius, guardLayer))
        {
            collider.GetComponent<GuardNPC>().AlarmOnPosition(transform.position);
        }
    }

    #region IBugable
    private int bugID;

    [System.Flags]
    public enum AlarmState : int
    {
        Hacked = 1,
        Disabled = 2,
        On = 4
    }

    private AlarmState state;
    public int State
    {
        get => (int)state;
        private set
        {
            this.state = (AlarmState)value;
            Disabled = (this.state & AlarmState.Disabled) > 0;
            if((this.state & AlarmState.On) > 0) SetOff();
        }
    }

    public bool Bugged => bugID >= 0;
    public IBugable.Type ObjectType => IBugable.Type.Alarm;
    public Transform GetBugPosition() => bugPosition;

    public void PlaceBug(int id)
    {
        bugID = id;
    }

    public bool TryChangeState(int state)
    {
        if ((state & (int)AlarmState.Hacked) == 0) return false;
        State = state;
        return true;
    }
    #endregion
}
