using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disturber : MonoBehaviour
{
    private List<ElectronicDevice> devices;
    public bool Disabled { get; private set; }

    private void Awake()
    {
        devices = new List<ElectronicDevice>(2);
        Disabled = false;
    }

    public void ElectronicDeviceEntered(ElectronicDevice ed)
    {
        if (Disabled)
            return;
        devices.Add(ed);
        ed.Disturbed = true;
    }

    public void ElectronicDeviceLeft(ElectronicDevice ed)
    {
        if (Disabled)
            return;
        devices.Remove(ed);
        ed.Disturbed = false;
    }

    public void Disable()
    {
        Disabled = true;
        foreach (ElectronicDevice ed in devices)
        {
            ed.Disturbed = false;
        }
        devices.Clear();
    }
}
