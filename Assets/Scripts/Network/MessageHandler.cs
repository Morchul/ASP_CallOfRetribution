using UnityEngine;

public abstract class MessageHandler : MonoBehaviour
{
    public GameEvent OnConnectionShutdown;
    public StringEvent OnChatMessageReceived;

    public const string CHAT_PREFIX = "CHAT:";

    [SerializeField]
    protected MessageTransmitter transmitter;

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
        else
        {
            Debug.Log("Received Message: " + message);
        }
    }

    public abstract void ChatMessageReceived(string message);
}
