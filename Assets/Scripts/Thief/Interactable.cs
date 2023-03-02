using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnInteractAction;

    [SerializeField]
    private bool isSuspicious;
    public bool IsSuspicious => isSuspicious;

    public void Interact()
    {
        OnInteractAction.Invoke();
    }
}
