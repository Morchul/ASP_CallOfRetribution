using UnityEngine;

public abstract class MessageHandler : MonoBehaviour
{
    [SerializeField]
    protected MessageTransmitter transmitter;

    [Header("Controller")]
    [SerializeField]
    protected GameController gameController;

    [Header("Events")]
    public GameEvent OnConnectionShutdown;
    public StringEvent OnChatMessageReceived;
    public IntEvent OnMissionSelect;
    public BugUpdateEvent OnBugUpdate;
    public GameEvent OnMissionLoaded;
    public GameEvent OnGameReady;

    protected virtual void Awake()
    {
        OnMissionLoaded.AddListener(() => transmitter.WriteToHost(MessageUtility.MISSION_LOADED));
    }

    public virtual void HandleMessage(string message)
    {
        if(message == MessageTransmitterCommands.SHUTDOWN)
        {
            OnConnectionShutdown.RaiseEvent();
        }
        else if (message.StartsWith(MessageUtility.CHAT_PREFIX))
        {
            ChatMessageReceived(message);
        }
        else if(message.StartsWith(MessageUtility.SELECT_MISSION_PREFIX))
        {
            SelectMissionReceived(message);
        }
        else if (message.StartsWith(MessageUtility.BUG_UPDATE_PREFIX))
        {
            BugUpdateMessageReceived(message);
        }
        else if (message.StartsWith(MessageUtility.GAME_READY))
        {
            OnGameReady.RaiseEvent();
        }
        else
        {
            Debug.Log("Received Message: " + message);
        }
    }

    public abstract void ChatMessageReceived(string message);
    public abstract void SelectMissionReceived(string message);
    public abstract void BugUpdateMessageReceived(string message);
}
