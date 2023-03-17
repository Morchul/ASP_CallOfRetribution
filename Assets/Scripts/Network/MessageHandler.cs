using UnityEngine;

public abstract class MessageHandler : MonoBehaviour
{
    [SerializeField]
    protected MessageTransmitter transmitter;

    [Header("Controller")]
    [SerializeField]
    protected GameController gameController;

    [Header("Events")]
    [SerializeField]
    protected StringEvent OnChatMessage;
    //Do not receive any of this events with a handler
    [Header("Only outgoing")]
    [SerializeField]
    protected NetworkGameEvent OnMissionLoaded;

    [Header("Only incoming")]
    //Do not send any of this events to the other client
    [SerializeField]
    protected NetworkGameEvent OnGameReady;
    [SerializeField]
    protected NetworkGameEvent OnConnectionShutdown;

    [Header("Send by Host")]
    [SerializeField]
    protected BoolEvent OnDroneConnectionStateChange;
    [SerializeField]
    protected FloatEvent OnScanOnCooldown;
    [SerializeField]
    protected Vector2Event OnExtractionPointActivate;
    [SerializeField]
    protected Vector2Event OnGuardScanned;
    [SerializeField]
    protected BugUpdateEvent OnBugUpdate;
    [SerializeField]
    protected IntEvent OnBugDenied;
    [SerializeField]
    protected IntEvent OnBugDisturbed;
    [SerializeField]
    protected BoolEvent OnMissionFinished;
    [SerializeField]
    protected IntEvent OnMissionSelect;
    [SerializeField]
    protected PosUpdateEvent OnPosUpdate;

    [Header("Send by Client")]
    [SerializeField]
    protected BugUpdateEvent OnBugUpdateRequest;
    [SerializeField]
    protected Vector2Event OnDroneMove;
    [SerializeField]
    protected NetworkGameEvent OnDroneScan;
    [SerializeField]
    protected NetworkGameEvent OnDrownFlare;

    public virtual void ForwardEvents()
    {
        if (NetworkManager.Instance.DEBUG_MODE) return;

        OnConnectionShutdown.ForwardEvent(transmitter);
        OnMissionLoaded.ForwardEvent(transmitter);
        OnChatMessage.ForwardEvent(transmitter);
    }

    public virtual void HandleReceivedMessage(string message)
    {
        Debug.Log("Received Message: " + message);

        if (OnConnectionShutdown.Listen(message)) return;
        if (OnChatMessage.Listen(message)) return;
        if (OnGameReady.Listen(message)) return;
    }
}
