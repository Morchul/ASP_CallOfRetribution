using UnityEngine;

public abstract class MessageHandler : MonoBehaviour
{
    [SerializeField]
    protected MessageTransmitter transmitter;

    [Header("Events")]
    public GameEvent OnConnectionShutdown;
    public StringEvent OnChatMessageReceived;
    public IntEvent OnMissionSelect;

    [Header("Controller")]
    [SerializeField]
    protected GameController gameController;

    public const string CHAT_PREFIX = "CHAT:";
    public const string SELECT_MISSION_PREFIX = "MISSION:";

    public virtual void HandleMessage(string message)
    {
        if(message == MessageTransmitterCommands.SHUTDOWN)
        {
            OnConnectionShutdown.RaiseEvent();
        }
        else if (message.StartsWith(CHAT_PREFIX))
        {
            ChatMessageReceived(message);
        }
        else if(message.StartsWith(SELECT_MISSION_PREFIX))
        {
            SelectMissionReceived(message);
        }
        else
        {
            Debug.Log("Received Message: " + message);
        }
    }

    public abstract void ChatMessageReceived(string message);
    public abstract void SelectMissionReceived(string message);
}
