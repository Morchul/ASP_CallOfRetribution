using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisturberArea : MonoBehaviour
{
    private Disturber disturber;
    private void Awake()
    {
        disturber = GetComponentInParent<Disturber>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (disturber.Disabled) return;

        Debug.Log("Enter disturber area: " + other.gameObject.name);

        ElectronicDevice ed = other.gameObject.GetComponent<ElectronicDevice>();
        if (ed != null)
        {
            disturber.ElectronicDeviceEntered(ed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (disturber.Disabled) return;
        ElectronicDevice ed = other.gameObject.GetComponent<ElectronicDevice>();
        if (ed != null)
        {
            disturber.ElectronicDeviceLeft(ed);
        }
    }
}
