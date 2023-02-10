using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour
{
    private List<IBugable> buggedObjects;

    private void Awake()
    {
        buggedObjects = new List<IBugable>();
    }
}
