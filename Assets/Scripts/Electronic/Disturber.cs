using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disturber : MonoBehaviour
{
    private List<ElectronicDevice> devices;
    private bool disabled;

    private void Awake()
    {
        devices = new List<ElectronicDevice>(2);
        disabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (disabled) return;
        ElectronicDevice ed = other.gameObject.GetComponent<ElectronicDevice>();
        if(ed != null)
        {
            devices.Add(ed);
            ed.Disturbed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (disabled) return;
        ElectronicDevice ed = other.gameObject.GetComponent<ElectronicDevice>();
        if (ed != null)
        {
            devices.Remove(ed);
            ed.Disturbed = false;
        }
    }

    public void Disable()
    {
        foreach(ElectronicDevice ed in devices)
        {
            ed.Disturbed = false;
        }
        devices.Clear();
    }
}
