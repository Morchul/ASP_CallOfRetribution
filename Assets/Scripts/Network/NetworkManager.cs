using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    [SerializeField]
    private int port;

    [Header("Host")]
    [SerializeField]
    private Host hostPrefab;

    [Header("Client")]
    [SerializeField]
    private Client clientPrefab;

    [Header("DEBUG")]
    public bool DEBUG_MODE = false;
    public bool DEBUG_HOST = false;

    public MessageTransmitter Transmitter { get; private set; }
    public IConnectionHandler ConnectionHandler { get; private set; }


    #region Singleton
    private static NetworkManager instance;

    public static NetworkManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            instance.Refresh();
            Debug.Log("Singleton NetworkManager does already exist. Reseting existing one!");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            if(!DEBUG_MODE)
                DontDestroyOnLoad(this);
            else
            {
                if (DEBUG_HOST)
                    CreateHost("");
                else
                    CreateClient("");
            }
        }
    }
    #endregion

    //Destroy child (host or client) every time player returns to main menu and a new NetworkManager would be created
    private void Refresh()
    {
        Destroy(gameObject.transform.GetChild(0).gameObject);
    }

    public void CreateHost(string hostIP)
    {
        ConnectionHandler = Instantiate(hostPrefab, this.transform);
        Transmitter = ConnectionHandler.GetTransmitter();
        if(!DEBUG_MODE)
            ConnectionHandler.StartHandler(hostIP, port);
    }

    public void CreateClient(string hostIP)
    {
        ConnectionHandler = Instantiate(clientPrefab, this.transform);
        Transmitter = ConnectionHandler.GetTransmitter();
        if (!DEBUG_MODE)
            ConnectionHandler.StartHandler(hostIP, port);
    }

    public void Shutdown()
    {
        if (ConnectionHandler != null)
            ConnectionHandler.Shutdown();
    }

    private void OnApplicationQuit()
    {
        Shutdown();
    }
}
