using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostMessageTransmitter : MessageTransmitter
{
    public override void WriteToClient(string message)
    {
        WriteOverSocket(message);
    }

    public override void WriteToHost(string message)
    {
        messageHandler.HandleMessage(message);
    }
}
