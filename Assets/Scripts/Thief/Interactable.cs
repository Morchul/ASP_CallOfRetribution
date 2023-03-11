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

    [SerializeField]
    private bool oneTimeUse;
    public bool OneTime => oneTimeUse;

    [SerializeField]
    private string actionName;
    public string ActionName => actionName;

    public void Interact()
    {
        OnInteractAction.Invoke();
        if (oneTimeUse)
            gameObject.SetActive(false);
    }
}
