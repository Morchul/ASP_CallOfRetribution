using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostMessageHandler : MessageHandler
{
    public override void ChatMessageReceived(string message)
    {
        OnChatMessageReceived.RaiseEvent(message.Substring(CHAT_PREFIX.Length));
        transmitter.WriteToClient(message);
    }
}
