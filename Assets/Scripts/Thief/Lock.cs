using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField]
    private bool lockedAtBeginning;

    public bool Locked { get; protected set; }

    protected virtual void Awake()
    {
        Locked = lockedAtBeginning;
    }

    public virtual void Unlock()
    {
        Locked = false;
    }

    public virtual void LockAction()
    {
        Locked = true;
    }
}
