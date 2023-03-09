using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefInteraction : MonoBehaviour
{
    [SerializeField]
    private ThiefTest thief;

    [SerializeField]
    private GameEvent OnSuspiciousActionExecuted;

    private Interactable currentInteractable;
    private Bugable currentBugable;
    private Bug currentBug;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            currentInteractable = other.GetComponent<Interactable>();
            currentBugable = other.GetComponent<Bugable>();
            Debug.Log("Entered interactable: " + currentInteractable.name);
        }
        if (other.CompareTag("Bug"))
        {
            currentBug = other.GetComponent<Bug>();
            Debug.Log("Found bug");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            currentInteractable = null;
            currentBugable = null;
            Debug.Log("Left interactable");
        }
        if (other.CompareTag("Bug"))
        {
            currentBug = null;
            Debug.Log("Left bug");
        }
    }

    private void Update()
    {
        if (currentInteractable != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentInteractable.Interact();
                if(currentInteractable.IsSuspicious)
                    OnSuspiciousActionExecuted.RaiseEvent();
            }
        }

        if(currentBugable != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                IBugable bugable = currentBugable.GetBugable();
                if (!bugable.Bugged)
                {
                    Bug bug = thief.GetBug();
                    if(bug != null)
                    {
                        bug.PlaceOn(bugable);
                        OnSuspiciousActionExecuted.RaiseEvent();
                    }  
                    else
                    {
                        //no bugs left
                    }
                }
            }
        }

        if(currentBug != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                currentBug.RemoveBy(thief);
                currentBug = null;
            }
        }
    }
}
