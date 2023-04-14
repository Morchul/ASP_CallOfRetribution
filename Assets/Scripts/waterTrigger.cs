using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterTrigger : MonoBehaviour
{
    [SerializeField]

    private BoolEvent OnMissionFinished;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Thief.TAG))
            OnMissionFinished.RaiseEvent(false);
    }
}
