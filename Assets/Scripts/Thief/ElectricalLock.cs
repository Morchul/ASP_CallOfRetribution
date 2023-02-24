using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalLock : Lock, IBugable
{
    private LockState state;
    public int State => (int)state;
    private int bugID;

    [System.Flags]
    public enum LockState
    {
        Hacked = 1,
        Open = 2
    }

    [SerializeField]
    private BugUpdateEvent OnBugUpdate;

    public bool Bugged => bugID >= 0;

    public IBugable.Type ObjectType => IBugable.Type.Lock;

    private void Awake()
    {
        OnBugUpdate.AddListener(BugUpdate);
        Locked = true;
    }

    private void BugUpdate(int id, IBugable.Type type, int state)
    {
        if (id != bugID) return;
        this.state = (LockState)state;
        if ((this.state & LockState.Open) > 0)
            Unlock();
        else
            LockAction();
    }

    public void PlaceBug(int id)
    {
        bugID = id;
    }
}
