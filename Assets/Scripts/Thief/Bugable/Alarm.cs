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

    public void SetOff()
    {
        if (Disabled) return;

        foreach(Collider collider in Physics.OverlapSphere(transform.position, alarmRadius, guardLayer))
        {
            Debug.Log("Alarm heard!");
            //collider.GetComponent<GuardNPC>().AlarmOnPosition(transform.position);
        }
    }

    #region IBugable
    private int bugID;

    [System.Flags]
    public enum AlarmState : int
    {
        Hacked = 1,
        Disabled = 2
    }

    private AlarmState state;
    public int State
    {
        get => (int)state;
        private set
        {
            this.state = (AlarmState)value;
            Disabled = (this.state & AlarmState.Disabled) > 0;
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
