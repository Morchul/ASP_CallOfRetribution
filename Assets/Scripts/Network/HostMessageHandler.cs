using UnityEngine;

public class HostMessageHandler : MessageHandler
{
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
