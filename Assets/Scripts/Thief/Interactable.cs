using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private float interactDistance;
    private float interactDistanceSquared;

    [Header("Debug")]
    [SerializeField]
    private Transform player;

    //[SerializeField]
    public UnityEvent OnInteractAction;

    private void Awake()
    {
        interactDistanceSquared = interactDistance * interactDistance;
    }

    private void Update()
    {
        if((player.position - transform.position).sqrMagnitude < interactDistanceSquared)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnInteractAction.Invoke();
            }
        }
    }
}
