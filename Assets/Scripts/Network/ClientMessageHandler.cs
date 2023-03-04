using UnityEngine;

public class ClientMessageHandler : MessageHandler
{
    public GameEvent OnConnectionRefused;

    public BoolEvent OnDroneConnectionStateChange;
    public StringEvent OnScanOnCooldown;

    public Vector2Event OnDronePosUpdate;
    public Vector2Event OnThiefPosUpdate;

    public Vector2Event OnExtractionPointDiscovered;

    public Vector2Event OnGuardScanned;

    public BugUpdateEvent OnBugUpdate;
    public GameEvent OnBugDenied;
    public GameEvent OnBugDisturbed;

    public override void HandleMessage(string message)
    {
        base.HandleMessage(message);

        if (message == MessageTransmitterCommands.REFUSE)
        {
            OnConnectionRefused.RaiseEvent();
        }
        else if (message.StartsWith(MessageUtility.SCAN_COOLDOWN_PREFIX))
        {
            OnScanOnCooldown.RaiseEvent(message.Substring(MessageUtility.SCAN_COOLDOWN_PREFIX.Length));
        }
        else if (message.StartsWith(MessageUtility.DRONE_STATE_CHANGE_PREFIX))
        {
            OnDroneConnectionStateChange.RaiseEvent(MessageUtility.GetDroneState(message));
        }
        else if (message.StartsWith(MessageUtility.DRONE_POS_PREFIX))
        {
            OnDronePosUpdate.RaiseEvent(MessageUtility.GetDronePosFromMessage(message));
        }
        else if (message.StartsWith(MessageUtility.THIEF_POS_PREFIX))
        {
            OnThiefPosUpdate.RaiseEvent(MessageUtility.GetThiefPosFromMessage(message));
        }
        else if(message.StartsWith(MessageUtility.SCAN_RESULT_PREFIX))
        {
            OnGuardScanned.RaiseEvent(MessageUtility.GetScanResultPos(message));
        }
        else if (message.StartsWith(MessageUtility.BUG_DENIED))
        {
            OnBugDenied.RaiseEvent();
        }
        else if (message.StartsWith(MessageUtility.BUG_DISTURBED))
        {
            OnBugDisturbed.RaiseEvent();
        }
        else if (message.StartsWith(MessageUtility.EXTRACTION_POS_PREFIX))
        {
            OnExtractionPointDiscovered.RaiseEvent(MessageUtility.GetExtractionPointPosFromMessage(message));
        }
    }

    public override void BugUpdateMessageReceived(string message)
    {
        int[] bugUpdateValues = MessageUtility.GetBugUpdateValues(message);
        OnBugUpdate.RaiseEvent(bugUpdateValues[0], (IBugable.Type)bugUpdateValues[1], bugUpdateValues[2]);
    }

    public override void ChatMessageReceived(string message)
    {
        OnChatMessageReceived.RaiseEvent(MessageUtility.GetChatMessage(message));
    }

    public override void SelectMissionReceived(string message)
    {
        OnMissionSelect.RaiseEvent(MessageUtility.GetMissionID(message));
        gameController.StartMission();
    }
}
