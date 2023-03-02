using UnityEngine;
using UnityEngine.Events;

public class Bugable : MonoBehaviour
{
    private IBugable bugable;

    private void Awake()
    {
        bugable = GetComponent<IBugable>();
    }

    public IBugable GetBugable() => bugable;
}
