using UnityEngine;

public class HostMessageHandler : MessageHandler
{
    /*[Header("Incoming")]
    [SerializeField]
    private Vector2Event OnDroneMoveEvent;
    [SerializeField]
    private GameEvent OnDrownScanEvent;
    [SerializeField]
    private GameEvent OnDrownFlareEvent;
    [SerializeField]
    private BugUpdateEvent OnBugUpdateRequestEvent;

    [Header("Outgoing")]
    [SerializeField]
    private BugUpdateEvent OnBugUpdateEvent;
    [SerializeField]
    private GameEvent OnMissionFinishedSuccessfully;
    [SerializeField]
    private GameEvent OnMissionFailed;
    [SerializeField]
    private Vector2Event OnGuardScanned;
    [SerializeField]
    private FloatEvent OnScanOnCooldown;
    [SerializeField]
    private Vector2Event OnExtractionPointActivate;
    [SerializeField]
    private BoolEvent OnDroneConnectionStateChanged;
    [SerializeField]
    private IntEvent OnBugDisturbedEvent;
    [SerializeField]
    private IntEvent OnBugDeniedEvent;
    [SerializeField]
    private PosUpdateEvent OnPosUpdateEvent;
    [SerializeField]
    private IntEvent OnMissionSelect;*/

    //private int missionLoadedCounter;

    public override void ForwardEvents()
    {
        base.ForwardEvents();
        //missionLoadedCounter = 0;

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

        /*OnBugUpdate.AddListener((id, type, state) => transmitter.WriteToClient(MessageUtility.CreateBugUpdateMessage(id, type, state)));
        OnMissionFinishedSuccessfully.AddListener(() => transmitter.WriteToClient(MessageUtility.MISSION_SUCCESSFUL));
        OnMissionFailed.AddListener(() => transmitter.WriteToClient(MessageUtility.MISSION_FAILED));
        OnGuardScanned.AddListener((guardPos) => transmitter.WriteToClient(MessageUtility.CreateScanResultMessage(guardPos)));
        OnScanOnCooldown.AddListener((cooldownTimer) => transmitter.WriteToClient(MessageUtility.CreateScanCooldownMessage(cooldownTimer)));
        OnExtractionPointActivate.AddListener((extractionPointPos) => transmitter.WriteToClient(MessageUtility.CreateExtractionPointPosMessage(extractionPointPos)));
        OnDroneConnectionStateChange.AddListener((disturbed) => transmitter.WriteToClient(MessageUtility.CreateDroneStateChangedMessage(disturbed)));
        OnBugDisturbed.AddListener((bugID) => transmitter.WriteToClient(MessageUtility.BUG_DISTURBED));
        OnBugDenied.AddListener((bugID) => transmitter.WriteToClient(MessageUtility.BUG_DENIED));
        OnPosUpdate.AddListener((posUpdate) => transmitter.WriteToClient(MessageUtility.CreatePosUpdateMessage(posUpdate)));
        OnMissionSelect.AddListener((missionID) => transmitter.WriteToClient(MessageUtility.CreateSelectMissionMessage(missionID)));*/
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

        /*if (message.StartsWith(MessageUtility.MOVE_DRONE_PREFIX))
        {
            OnDroneMove.RaiseEvent(MessageUtility.GetCoordinates(message));
        }

        else if (message.Equals(MessageUtility.SCAN_DRONE))
        {
            OnDroneScan.RaiseEvent();
        }

        else if (message.Equals(MessageUtility.FLARE_DRONE))
        {
            OnDrownFlare.RaiseEvent();
        }
        else if (message.Equals(MessageUtility.MISSION_FAILED))
        {
            OnMissionFailed.RaiseEvent();
        }*/
    }

    /*public override void ChatMessageReceived(string message)
    {
        OnChatMessageReceived.RaiseEvent(MessageUtility.GetChatMessage(message));
        transmitter.WriteToClient(message);
    }

    public override void BugUpdateMessageReceived(string message)
    {
        int[] bugUpdateValues = MessageUtility.GetBugUpdateValues(message);
        OnBugUpdateRequest.RaiseEvent(bugUpdateValues[0], (IBugable.Type)bugUpdateValues[1], bugUpdateValues[2]);
    }*/
}
