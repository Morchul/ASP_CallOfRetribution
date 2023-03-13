using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTrigger : MonoBehaviour
{
    [SerializeField]
    private ThiefTutorial tutorial;

    [SerializeField]
    private int id;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Thief.TAG))
            tutorial.EnterTrigger(id);
    }
}
