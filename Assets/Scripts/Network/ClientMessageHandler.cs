using UnityEngine;

public class ClientMessageHandler : MessageHandler
{
    [Header("Unique client events (Incoming)")]
    [SerializeField]
    protected GameEvent OnConnectionRefused;

    public override void ForwardEvents()
    {
        base.ForwardEvents();

        OnBugUpdateRequest.ForwardEvent(transmitter);
        OnDroneMove.ForwardEvent(transmitter);
        OnDroneScan.ForwardEvent(transmitter);
        OnDrownFlare.ForwardEvent(transmitter);
    }

    public override void HandleReceivedMessage(string message)
    {
        base.HandleReceivedMessage(message);

        if (OnDroneConnectionStateChange.Listen(message)) return;
        if (OnScanOnCooldown.Listen(message)) return;
        if (OnExtractionPointActivate.Listen(message)) return;
        if (OnGuardScanned.Listen(message)) return;
        if (OnBugUpdate.Listen(message)) return;
        if (OnBugDenied.Listen(message)) return;
        if (OnBugDisturbed.Listen(message)) return;
        if (OnMissionFinishedSuccessfully.Listen(message)) return;
        if (OnMissionFailed.Listen(message)) return;
        if (OnMissionSelect.Listen(message)) return;
        if (OnPosUpdate.Listen(message)) return;
    }
}
