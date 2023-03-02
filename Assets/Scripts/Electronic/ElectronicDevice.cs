using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicDevice : MonoBehaviour
{
    private bool disturbed;
    public virtual bool Disturbed
    {
        get => disturbed;
        set
        {
            disturbed = value;
            AfterDisturbedChange();
        }
    }

    private void Awake()
    {
        disturbed = false;
    }

    protected virtual void AfterDisturbedChange()
    {

    }
}
