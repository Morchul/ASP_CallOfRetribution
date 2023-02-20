using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    private LockState state;

    [System.Flags]
    public enum LockState
    {
        Hacked = 1,
        Open = 2
    }
}
