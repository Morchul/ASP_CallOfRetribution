using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConnectionHandler
{
    public void Shutdown();
    public void StartHandler(string hostIP, int port);

    public bool Running { get; }

    public MessageTransmitter GetTransmitter();
    public bool IsHost();
    public string GetChatName();
}
