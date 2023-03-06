using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Client : MonoBehaviour, IConnectionHandler
{
    [SerializeField]
    private MessageTransmitter messageTransmitter;
    public MessageTransmitter GetTransmitter() => messageTransmitter;

    [Header("Events")]
    public GameEvent OnConnectionLost;
    public GameEvent OnConnectionEstablished;
    public GameEvent OnConnectionShutdown;
    public GameEvent OnConnectionRefused;
    public GameEvent OnConnectionFailure;

    public bool Running { get; private set; }
    public string IP { get; private set; }
    public int Port { get; private set; }

    private void Awake()
    {
        Running = false;
        Refresh();
    }

    public void Refresh()
    {
        OnConnectionShutdown.AddListener(ShutdownCommandReceived);
        OnConnectionRefused.AddListener(ConnectionRefusedByHost);
    }

    public void StartHandler(string hostIP, int port)
    {
        if (Running)
        {
            Debug.Log("Client is already Running");
            return;
        }

        Socket clientSocket = null;
        try
        {
            Debug.Log("Create Client...");
            IP = hostIP;
            Port = port;
            IPAddress ipAddress = IPAddress.Parse(IP);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, Port);

            clientSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            Running = true;
            Debug.Log("Created Client successfully");

            Debug.Log("Try Connect to host with ip: " + hostIP + " and port: " + port + " ...");
            clientSocket.Connect(remoteEP);
            if (clientSocket.Connected)
            {
                OnConnectionLost.AddListener(ConnectionLost);
                messageTransmitter.SetConnectedSocket(clientSocket);
                Running = true;
                OnConnectionEstablished.RaiseEvent();
                Debug.Log("Successfully connected to host!");
            }
            else
            {
                Debug.LogError("Can not connect to host: " + hostIP + " with port: " + port);
                clientSocket.Close();
            }
        }
        catch (Exception e)
        {
            OnConnectionFailure.RaiseEvent();
            Debug.LogError("Error while trying to connect to host: " + hostIP + " :: " + e.Message + "\n" + e.StackTrace);
            if (clientSocket != null)
            {
                clientSocket.Close();
            }
        }
    }

    public void Shutdown()
    {
        if (Running)
        {
            Running = false;

            Debug.Log("Client shutdown");
            OnConnectionLost.RemoveListener(ConnectionLost);
            messageTransmitter.SendShutdown();
            OnConnectionShutdown.RaiseEvent();
        }
    }

    private void ConnectionRefusedByHost()
    {
        Debug.Log("Connection refused by host");
        OnConnectionShutdown.RaiseEvent();
    }

    private void ShutdownCommandReceived()
    {
        if (Running)
        {
            Running = false;
            Debug.Log("Client shutdown");
            OnConnectionLost.RemoveListener(ConnectionLost);
        }
    }

    public void SetTransmitter(MessageTransmitter transmitter)
    {
        this.messageTransmitter = transmitter;
    }

    public bool IsHost() => false;
    public string GetChatName() => "Commander";

    private void ConnectionLost()
    {
        Debug.LogWarning("Client lost connection to host!");
        Shutdown();
    }
}
