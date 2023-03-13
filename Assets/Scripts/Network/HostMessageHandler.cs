using UnityEngine;

public class HostMessageHandler : MessageHandler
{

    public override void ForwardEvents()
    {
        base.ForwardEvents();

        OnDroneConnectionStateChange.ForwardEvent(transmitter);
        OnScanOnCooldown.ForwardEvent(transmitter);
        OnExtractionPointActivate.ForwardEvent(transmitter);
        OnGuardScanned.ForwardEvent(transmitter);
        OnBugUpdate.ForwardEvent(transmitter);
        OnBugDenied.ForwardEvent(transmitter);
        OnBugDisturbed.ForwardEvent(transmitter);
        OnMissionFinishedSuccessfully.ForwardEvent(transmitter);
        OnMissionFailed.ForwardEvent(transmitter);
        OnMissionSelect.ForwardEvent(transmitter);
        OnPosUpdate.ForwardEvent(transmitter);

        OnGameReady.ForwardEvent(transmitter);
    }

    public override void HandleReceivedMessage(string message)
    {
        base.HandleReceivedMessage(message);
        if (OnBugUpdateRequest.Listen(message)) return;
        if (OnDroneMove.Listen(message)) return;
        if (OnDroneScan.Listen(message)) return;
        if (OnDrownFlare.Listen(message)) return;
        if (OnMissionLoaded.Listen(message)) return;
        if (OnMissionFailed.Listen(message)) return;
    }
}
