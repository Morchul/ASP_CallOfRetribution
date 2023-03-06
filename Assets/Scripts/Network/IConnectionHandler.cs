using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConnectionHandler
{
    public void Shutdown();
    public void StartHandler(string hostIP, int port);

    //Called everytime a new Network manager get's created
    public void Refresh();

    public string IP { get; }
    public int Port { get; }

    public bool Running { get; }

    public MessageTransmitter GetTransmitter();
    public bool IsHost();
    public string GetChatName();
}
