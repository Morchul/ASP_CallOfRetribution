using UnityEngine;

public class HostMessageHandler : MessageHandler
{
    [SerializeField]
    private Vector2Event OnDroneMoveMessage;
    [SerializeField]
    private GameEvent OnDrownScanMessage;
    [SerializeField]
    private GameEvent OnDrownFlareMessage;
    [SerializeField]
    private BugUpdateEvent OnBugUpdateRequestEvent;
    [SerializeField]
    private BugUpdateEvent OnBugUpdateEvent;

    private int missionLoadedCounter;

    protected override void Awake()
    {
        base.Awake();
        missionLoadedCounter = 0;

        OnBugUpdateEvent.AddListener((id, type, state) => transmitter.WriteToClient(MessageUtility.CreateBugUpdateMessage(id, type, state)));
    }

    public override void HandleMessage(string message)
    {
        base.HandleMessage(message);

        if (message.StartsWith(MessageUtility.MOVE_DRONE_PREFIX))
        {
            OnDroneMoveMessage.RaiseEvent(MessageUtility.GetCoordinates(message));
        }

        else if (message.StartsWith(MessageUtility.SCAN_DRONE))
        {
            OnDrownScanMessage.RaiseEvent();
        }

        else if (message.StartsWith(MessageUtility.FLARE_DRONE))
        {
            OnDrownFlareMessage.RaiseEvent();
        }

        else if (message.StartsWith(MessageUtility.MISSION_LOADED))
        {
            if(++missionLoadedCounter == 2)
            {
                transmitter.WriteToClient(MessageUtility.GAME_READY);
                transmitter.WriteToHost(MessageUtility.GAME_READY); //Can be replaced through direct RaiseEvent call
            }
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
        int[] bugUpdateValues = MessageUtility.GetBugUpdateValues(message);
        OnBugUpdateRequestEvent.RaiseEvent(bugUpdateValues[0], (IBugable.Type)bugUpdateValues[1], bugUpdateValues[2]);
    }
}
