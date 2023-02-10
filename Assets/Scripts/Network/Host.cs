using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Host : MonoBehaviour, IConnectionHandler
{
    [SerializeField]
    private MessageTransmitter messageTransmitter;
    public MessageTransmitter GetTransmitter() => messageTransmitter;

    private Socket serverSocket;
    private Socket clientSocket;

    private Thread listeningThread;
    private Exception threadException;

    private bool acceptIncomingConnection = false;
    public bool Running { get; private set; }

    public GameEvent OnConnectionLost;
    public GameEvent OnConnectionEstablished;
    public GameEvent OnConnectionShutdown;

    private void Start()
    {
        OnConnectionLost.AddListener(ConnectionLost);
        OnConnectionShutdown.AddListener(ConnectionLost);
        Running = false;
    }

    public void StartHandler(string hostIP, int port)
    {
        if (Running)
        {
            Debug.Log("Host is already Running");
            return;
        }

        Debug.Log("Create Host...");
        
        IPAddress ipAddress = IPAddress.Parse(hostIP);
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

        try
        {
            serverSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(localEndPoint);
            serverSocket.Listen(1);

            Running = true;

            Debug.Log("Start listening for connections on ip: " + ipAddress.ToString() + " and port: " + port + " ...");
            acceptIncomingConnection = true;
            listeningThread = new Thread(new ThreadStart(WaitForConnection));
            listeningThread.Start();
        }
        catch (Exception e)
        {
            Debug.LogError("Error while trying to host game: " + e.Message + "\n" + e.StackTrace);
            Running = false;
            serverSocket.Close();
            serverSocket = null;
        }
    }

    public void Shutdown()
    {
        if (Running)
        {
            Running = false;

            messageTransmitter.SendShutdown();
            OnConnectionShutdown.RaiseEvent();

            try
            {
                serverSocket.Shutdown(SocketShutdown.Both);
            }
            finally
            {
                serverSocket.Close();
                serverSocket = null;
            }
        }
    }

    public void SetTransmitter(MessageTransmitter transmitter)
    {
        this.messageTransmitter = transmitter;
    }

    public bool IsHost() => true;
    public string GetChatName() => "Thief";

    private void WaitForConnection()
    {
        threadException = null;
        while (Running && threadException == null)
        {
            try
            {
                Debug.Log("Wait for connection...");
                Socket socket = serverSocket.Accept();
                if (acceptIncomingConnection)
                {
                    Debug.Log("Connection accepted");
                    clientSocket = socket;
                }
                else
                {
                    Debug.Log("Connection refused, disconnect");
                    //using (NetworkStream ns = new NetworkStream(socket))
                    //{
                    //    using (StreamWriter writer = new StreamWriter(ns))
                    //    {
                    //        writer.WriteLine(MessageTransmitterCommands.REFUSE);
                    //    }
                    //}
                    socket.Send(Encoding.ASCII.GetBytes(MessageTransmitterCommands.REFUSE + "\n"));
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Disconnect(false);
                    socket.Dispose();
                }
            }
            catch (Exception e)
            {
                Debug.Log("Thread exception: " + e.Message + "/n" + e.StackTrace);
                threadException = e;
            }
        }
        
    }

    private void Update()
    {
        if (acceptIncomingConnection)
        {
            if(clientSocket != null)
            {
                if (threadException == null)
                {
                    ConnectionEstablished(clientSocket);
                    acceptIncomingConnection = false;
                    clientSocket = null;
                }
                else
                {
                    Debug.LogError("ERROR while trying to accept connection: " + threadException.Message + "\n" + threadException.StackTrace);
                }
            }
        }
    }

    private void ConnectionEstablished(Socket socket)
    {
        Debug.Log("Connection established!");
        OnConnectionEstablished.RaiseEvent();

        messageTransmitter.SetConnectedSocket(socket);
    }

    private void ConnectionLost()
    {
        if (!acceptIncomingConnection)
        {
            Debug.LogWarning("Host lost connection to client!");
            acceptIncomingConnection = true;
        }
    }
}
