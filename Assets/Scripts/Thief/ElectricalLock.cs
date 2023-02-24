using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalLock : Lock
{
    private LockState state;

    [System.Flags]
    public enum LockState
    {
        Hacked = 1,
        Open = 2
    }

    [SerializeField]
    private BugUpdateEvent OnBugUpdate;

    private void Awake()
    {
        OnBugUpdate.AddListener(BugUpdate);
    }

    private void BugUpdate(int id, IBugable.Type type, int state)
    {
        this.state = (LockState)state;
        if ((this.state & LockState.Open) > 0)
            Unlock();
        else
            LockAction();
    }
}
