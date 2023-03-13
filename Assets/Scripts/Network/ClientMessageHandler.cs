using UnityEngine;

public class ClientMessageHandler : MessageHandler
{
    [Header("Unique client events (Incoming)")]
    [SerializeField]
    protected GameEvent OnConnectionRefused;

    public override void ForwardEvents()
    {
        base.ForwardEvents();
        //OnBugUpdateRequest.AddListener((bugUpdate) => transmitter.WriteToHost(MessageUtility.CreateBugUpdateMessage(bugUpdate.ID, bugUpdate.Type, bugUpdate.Status)));
        //OnDroneMove.AddListener((moveCoordinates) => transmitter.WriteToHost(MessageUtility.CreateMoveDroneMessage(moveCoordinates)));
        //OnDroneScan.AddListener(() => transmitter.WriteToHost(MessageUtility.SCAN_DRONE));
        //OnDrownFlare.AddListener(() => transmitter.WriteToHost(MessageUtility.FLARE_DRONE));

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

        /*if (message == MessageTransmitterCommands.REFUSE)
        {
            OnConnectionRefused.RaiseEvent();
        }
        else if (message.StartsWith(MessageUtility.SCAN_COOLDOWN_PREFIX))
        {
            OnScanOnCooldown.RaiseEvent(MessageUtility.GetCooldownTime(message));
        }
        else if (message.StartsWith(MessageUtility.DRONE_STATE_CHANGE_PREFIX))
        {
            OnDroneConnectionStateChange.RaiseEvent(MessageUtility.GetDroneState(message));
        }
        else if (message.StartsWith(MessageUtility.POS_UPDATE_PREFIX))
        {
            OnPosUpdate.RaiseEvent(MessageUtility.GetPosUpdateInfoFromMessage(message));
        }
        else if(message.StartsWith(MessageUtility.SCAN_RESULT_PREFIX))
        {
            OnGuardScanned.RaiseEvent(MessageUtility.GetScanResultPos(message));
        }
        else if (message.Equals(MessageUtility.BUG_DENIED))
        {
            OnBugDenied.RaiseEvent(MessageUtility.GetIntAfterPrefix(MessageUtility.BUG_DENIED, message));
        }
        else if (message.Equals(MessageUtility.BUG_DISTURBED))
        {
            OnBugDisturbed.RaiseEvent(MessageUtility.GetIntAfterPrefix(MessageUtility.BUG_DISTURBED, message));
        }
        else if (message.StartsWith(MessageUtility.EXTRACTION_POS_PREFIX))
        {
            OnExtractionPointActivate.RaiseEvent(MessageUtility.GetExtractionPointPosFromMessage(message));
        }
        else if (message.Equals(MessageUtility.MISSION_SUCCESSFUL))
        {
            OnMissionFinishedSuccessfully.RaiseEvent();
        }
        else if (message.Equals(MessageUtility.MISSION_FAILED))
        {
            OnMissionFailed.RaiseEvent();
        }
        else if (message.StartsWith(MessageUtility.SELECT_MISSION_PREFIX))
        {
            OnMissionSelect.RaiseEvent(MessageUtility.GetMissionID(message));
            gameController.StartMission();
        }*/
    }

    /*public override void BugUpdateMessageReceived(string message)
    {
        int[] bugUpdateValues = MessageUtility.GetBugUpdateValues(message);
        OnBugUpdate.RaiseEvent(bugUpdateValues[0], (IBugable.Type)bugUpdateValues[1], bugUpdateValues[2]);
    }

    public override void ChatMessageReceived(string message)
    {
        OnChatMessageReceived.RaiseEvent(MessageUtility.GetChatMessage(message));
    }*/
}
