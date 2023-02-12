using UnityEngine;

public class HostMessageHandler : MessageHandler
{
    public override void ChatMessageReceived(string message)
    {
        OnChatMessageReceived.RaiseEvent(message.Substring(CHAT_PREFIX.Length));
        transmitter.WriteToClient(message);
    }

    public override void SelectMissionReceived(string message)
    {
        transmitter.WriteToClient(message);
        OnMissionSelect.RaiseEvent(int.Parse(message.Substring(SELECT_MISSION_PREFIX.Length)));
        gameController.StartMission();
    }
}
