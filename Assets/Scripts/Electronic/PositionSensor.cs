using UnityEngine;

public class PositionSensor : ElectronicDevice
{
    [SerializeField]
    protected char identifier;

    [SerializeField]
    private PosUpdateEvent OnPosUpdateEvent;

    public void SendPosUpdate()
    {
        if (!Disturbed)
            OnPosUpdateEvent.RaiseEvent(identifier, transform.position);
    }
}
