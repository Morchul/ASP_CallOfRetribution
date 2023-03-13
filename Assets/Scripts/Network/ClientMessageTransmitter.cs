using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMessageTransmitter : MessageTransmitter
{
    public override void WriteToClient(string message)
    {
        messageHandler.HandleReceivedMessage(message);
    }

    public override void WriteToHost(string message)
    {
        WriteOverSocket(message);
    }
}
