public class ClientMessageHandler : MessageHandler
{
    public GameEvent OnConnectionRefused;

    public override void BugUpdateMessageReceived(string message)
    {
        int[] bugUpdateValues = MessageUtility.GetBugUpdateValues(message);
        OnBugUpdate.RaiseEvent(bugUpdateValues[0], (IBugable.Type)bugUpdateValues[1], bugUpdateValues[2]);
    }

    public override void ChatMessageReceived(string message)
    {
        OnChatMessageReceived.RaiseEvent(MessageUtility.GetChatMessage(message));
    }

    public override void HandleMessage(string message)
    {
        base.HandleMessage(message);

        if (message == MessageTransmitterCommands.REFUSE)
        {
            OnConnectionRefused.RaiseEvent();
        }
    }

    public override void SelectMissionReceived(string message)
    {
        OnMissionSelect.RaiseEvent(MessageUtility.GetMissionID(message));
        gameController.StartMission();
    }
}
