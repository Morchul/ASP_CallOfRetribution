using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSensor : ElectronicDevice
{
    protected System.Func<Vector3, string> UpdateCreateFunc;

    public void SendPosUpdate()
    {
        if(!Disturbed)
            NetworkManager.Instance.Transmitter.WriteToClient(UpdateCreateFunc(transform.position));
    }
}
