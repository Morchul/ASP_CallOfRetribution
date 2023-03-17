using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public static class MessageTransmitterCommands
{
    public const string REFUSE = "Refuse";
}

public abstract class MessageTransmitter : MonoBehaviour
{
    [SerializeField]
    protected MessageHandler messageHandler;

    [Header("Events")]
    public GameEvent OnConnectionLost;
    public GameEvent OnConnectionShutdown;

    private Socket clientSocket;

    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;

    private Thread listener;
    private Exception listenerException;

    private Queue<string> receivedMessages;

    public bool Running { get; private set; }

    private void Awake()
    {
        Running = false;
        receivedMessages = new Queue<string>();
        Refresh();
    }

    public void Refresh()
    {
        if (NetworkManager.Instance.DEBUG_MODE) return;

        OnConnectionShutdown.AddListener(ConnectionShutdown);
        messageHandler.ForwardEvents();
    }

    public void SetConnectedSocket(Socket socket)
    {
        clientSocket = socket;

        stream = new NetworkStream(socket);
        writer = new StreamWriter(stream);
        reader = new StreamReader(stream);

        Running = true;
        StartListening();
    }

    private void StartListening()
    {
        listenerException = null;
        receivedMessages.Clear();

        listener = new Thread(new ThreadStart(ReceiveMessages));
        listener.Start();
    }

    private void ReceiveMessages()
    {
        string receivedMessage = null;
        while (Running)
        {
            try
            {
                receivedMessage = reader.ReadLine();
            }
            catch (Exception e)
            {
                if (Running)
                    listenerException = e;
            }

            if (receivedMessage != null && Running)
            {
                Monitor.Enter(receivedMessages);
                try
                {
                    receivedMessages.Enqueue(receivedMessage);
                }
                finally
                {
                    Monitor.Exit(receivedMessages);
                }
            }
        }
    }

    private void Update()
    {
        if (!Running) return;

        if (listenerException != null)
        {
            Debug.LogError("Error while reading message! " + listenerException.Message + "\n" + listenerException.StackTrace);
            ConnectionError();
            listenerException = null;
        }

        if (receivedMessages.Count > 0)
        {
            if (Monitor.TryEnter(receivedMessages, 2))
            {
                string message = null;
                try
                {
                    message = receivedMessages.Dequeue();
                }
                finally
                {
                    Monitor.Exit(receivedMessages);
                    if (message != null)
                        messageHandler.HandleReceivedMessage(message);
                }
            }
        }
    }

    protected void WriteOverSocket(string message)
    {
        if (!Running)
        {
            Debug.LogWarning("Can't write, Socket is not connected");
            return;
        }

        try
        {
            writer.WriteLine(message);
            writer.Flush();
        }
        catch (IOException ioE)
        {
            Debug.LogError("Error while writing message! " + ioE.Message + "\n" + ioE.StackTrace);
            ConnectionError();
        }
    }

    public abstract void WriteToClient(string message);
    public abstract void WriteToHost(string message);
    public virtual void WriteToOther(string message)
    {
        WriteOverSocket(message);
    }

    private void ConnectionShutdown()
    {
        Close();
    }

    private void ConnectionError()
    {
        Close();
        OnConnectionLost.RaiseEvent();
    }

    private void Close()
    {
        if (Running)
        {
            Running = false;

            writer.Close();
            reader.Close();
            stream.Close();

            clientSocket.Close();
        }
    }
}
