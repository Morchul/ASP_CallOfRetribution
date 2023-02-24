using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBugable
{
    bool Bugged { get; }
    int State { get; }
    Type ObjectType { get; }
    void PlaceBug(int id);

    [System.Serializable]
    public enum Type
    {
        None,
        Lock,
    }
}
