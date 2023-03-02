using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBugable
{
    bool Bugged { get; }
    int State { get; set; }
    Type ObjectType { get; }

    void PlaceBug(int id);
    Transform GetBugPosition();

    [System.Serializable]
    public enum Type
    {
        None,
        Lock,
    }
}
