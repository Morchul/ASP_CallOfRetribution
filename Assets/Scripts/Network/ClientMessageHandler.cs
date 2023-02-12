public class ClientMessageHandler : MessageHandler
{
    public GameEvent OnConnectionRefused;

    public override void ChatMessageReceived(string message)
    {
        OnChatMessageReceived.RaiseEvent(message.Substring(CHAT_PREFIX.Length));
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
        OnMissionSelect.RaiseEvent(int.Parse(message.Substring(SELECT_MISSION_PREFIX.Length)));
        gameController.StartMission();
    }
}
