using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBugable
{
    bool Bugged { get; }
    Type ObjectType { get; }
    void Hack();
    void PlaceBug();

    [System.Serializable]
    public enum Type
    {
        None,
        Lock,
    }
}
