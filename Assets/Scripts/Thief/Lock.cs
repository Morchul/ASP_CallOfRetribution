using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public bool Locked { get; protected set; }

    public virtual void Unlock()
    {
        Locked = false;
    }

    public virtual void LockAction()
    {
        Locked = true;
    }
}
