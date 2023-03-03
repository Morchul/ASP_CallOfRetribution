using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBugable
{
    bool Bugged { get; }
    int State { get; }
    Type ObjectType { get; }

    bool TryChangeState(int state);

    void PlaceBug(int id);
    Transform GetBugPosition();

    [System.Serializable]
    public enum Type
    {
        None,
        Lock,
        Alarm,
    }
}
