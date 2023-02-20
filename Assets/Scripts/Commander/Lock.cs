using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    private LockState state;

    [System.Flags]
    public enum LockState
    {
        Hacked = 0x01,
        Open = 0x10
    }
}
