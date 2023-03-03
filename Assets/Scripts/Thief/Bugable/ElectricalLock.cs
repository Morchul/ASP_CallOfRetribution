using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalLock : Lock, IBugable
{
    [SerializeField]
    private Transform bugPosition;

    private int bugID;

    [System.Flags]
    public enum LockState
    {
        Hacked = 1,
        Open = 2
    }

    private LockState state;
    public int State
    {
        get => (int)state;
        private set
        {
            this.state = (LockState)value;
            if ((this.state & LockState.Open) > 0)
                Unlock();
            else
                LockAction();
        }
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

    public bool TryChangeState(int state)
    {
        if ((state & (int)LockState.Hacked) == 0) return false;
        State = state;
        return true;
    }
}
