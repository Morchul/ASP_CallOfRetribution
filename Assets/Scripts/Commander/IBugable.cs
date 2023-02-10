using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBugable
{
    bool Bugged { get; }
    void Hack();
    void PlaceBug();
}
