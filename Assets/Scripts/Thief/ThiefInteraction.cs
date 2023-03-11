using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThiefInteraction : MonoBehaviour
{
    [SerializeField]
    private ThiefTest thief;

    [SerializeField]
    private GameEvent OnSuspiciousActionExecuted;

    [SerializeField]
    private TMP_Text interactionText;

    private Interactable currentInteractable;
    private Bugable currentBugable;
    private Bug currentBug;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            Interactable tmp = other.GetComponent<Interactable>();
            if (tmp != null)
                currentInteractable = tmp;

            Bugable bTmp = other.GetComponent<Bugable>();
            if(bTmp != null)
                currentBugable = bTmp;

            UpdateInteractText();
        }
        if (other.CompareTag("Bug"))
        {
            currentBug = other.GetComponent<Bug>();
            UpdateInteractText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            Interactable tmp = other.GetComponent<Interactable>();
            if (tmp != null)
                currentInteractable = null;

            Bugable bTmp = other.GetComponent<Bugable>();
            if (bTmp != null)
                currentBugable = null;

            UpdateInteractText();
        }
        if (other.CompareTag("Bug"))
        {
            currentBug = null;
            UpdateInteractText();
        }
    }

    private void UpdateInteractText()
    {
        string text = "";
        if (currentInteractable != null)
        {
            text += currentInteractable.ActionName + " (E)\n";
        }
        if (currentBug != null)
        {
            text += "Retrieve Bug (Q)";
        }
        if(currentBugable != null && !currentBugable.GetBugable().Bugged)
        {
            text += "Place Bug on " + currentBugable.name + " (Q)\n";
        }

        interactionText.text = text;
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
                if (currentInteractable.OneTime)
                    currentInteractable = null;
                UpdateInteractText();
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
                    UpdateInteractText();
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
