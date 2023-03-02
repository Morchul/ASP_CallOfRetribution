using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalLock : Lock, IBugable
{
    [SerializeField]
    private Transform bugPosition;

    private LockState state;
    public int State
    {
        get => (int)state;
        set
        {
            this.state = (LockState)value;
            if ((this.state & LockState.Open) > 0)
                Unlock();
            else
                LockAction();
        }
    }

    private int bugID;

    [System.Flags]
    public enum LockState
    {
        Hacked = 1,
        Open = 2
    }

    protected override void Awake()
    {
        base.Awake();
        bugID = -1;
    }

    public bool Bugged => bugID >= 0;
    public IBugable.Type ObjectType => IBugable.Type.Lock;
    public Transform GetBugPosition() => bugPosition;

    public void PlaceBug(int id)
    {
        bugID = id;
    }

    
}
