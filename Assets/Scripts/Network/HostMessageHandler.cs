using UnityEngine;

public class HostMessageHandler : MessageHandler
{
    [SerializeField]
    private Vector2Event OnDroneMoveMessage;
    [SerializeField]
    private GameEvent OnDrownScanMessage;
    [SerializeField]
    private GameEvent OnDrownFlareMessage;

    public override void HandleMessage(string message)
    {
        base.HandleMessage(message);

        if (message.StartsWith(MessageUtility.MOVE_DRONE_PREFIX))
        {
            OnDroneMoveMessage.RaiseEvent(MessageUtility.GetCoordinates(message));
        }

        if (message.StartsWith(MessageUtility.SCAN_DRONE_PREFIX))
        {
            OnDrownScanMessage.RaiseEvent();
        }

        if (message.StartsWith(MessageUtility.FLARE_DRONE_PREFIX))
        {
            OnDrownFlareMessage.RaiseEvent();
        }
    }

    public override void ChatMessageReceived(string message)
    {
        OnChatMessageReceived.RaiseEvent(MessageUtility.GetChatMessage(message));
        transmitter.WriteToClient(message);
    }

    public override void SelectMissionReceived(string message)
    {
        transmitter.WriteToClient(message);
        OnMissionSelect.RaiseEvent(MessageUtility.GetMissionID(message));
        gameController.StartMission();
    }

    public override void BugUpdateMessageReceived(string message)
    {
        transmitter.WriteToClient(message);
        int[] bugUpdateValues = MessageUtility.GetBugUpdateValues(message);
        OnBugUpdate.RaiseEvent(bugUpdateValues[0], (IBugable.Type)bugUpdateValues[1], bugUpdateValues[2]);
    }
}
